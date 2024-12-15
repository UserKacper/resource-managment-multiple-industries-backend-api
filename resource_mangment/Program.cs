using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using resource_manager_db.Db_connector;
using resource_manager_db.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddDbContext<DbConnector>(options =>
      options.UseNpgsql(builder.Configuration["dbConString"]));

builder.Services.AddIdentity<Employee, Role>()
    .AddEntityFrameworkStores<DbConnector>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
