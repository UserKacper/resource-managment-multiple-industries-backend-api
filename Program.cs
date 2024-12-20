using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using resource_manager_db.Db_connector;
using resource_manager_db.Models;
using resource_mangment.Logic.Mapper;
using resource_mangment.Logic.Services;
using resource_mangment.Logic.Services.ASLoginErrorHandling;
using resource_mangment.Logic.Services.ASRegisterErrorHandling;
using resource_mangment.Logic.TokenService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description =
                "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});

builder.Services.AddScoped<MapperProfile>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IAuthServiceHandlingRegisterErrors, AuthServiceHandlingRegisterErrors>();
builder.Services.AddScoped<IAuthServiceLoginHandling, AuthServiceLoginHandling>();

builder.Configuration.AddUserSecrets<Program>();

// DbContext and Identity setup
builder.Services.AddDbContext<Database_ctx>(options =>
    options.UseNpgsql(builder.Configuration["database_connectionstring"])
);

builder
    .Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<Database_ctx>()
    .AddDefaultTokenProviders();

// JWT Authentication setup
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("localhost")),
        };
    });

var app = builder.Build();

// Seed the "Owner" role after building the app
await SeedRolesAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

async Task SeedRolesAsync(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roleName = "Owner";

        var roleExist = await roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var role = new IdentityRole(roleName);
            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                Console.WriteLine($"Role '{roleName}' created successfully.");
            }
            else
            {
                Console.WriteLine(
                    $"Error creating role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
            }
        }
        else
        {
            Console.WriteLine($"Role '{roleName}' already exists.");
        }
    }
}

app.Run();
