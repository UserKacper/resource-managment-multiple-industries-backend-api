using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resource_manager_db.Db_connector;
using resource_manager_db.Models;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.DTO_s;
using resource_mangment.Logic.ServerResponseModel;
using resource_mangment.Logic.Services.ASLoginErrorHandling;
using resource_mangment.Logic.Services.ASRegisterErrorHandling;
using resource_mangment.Logic.TokenService;

namespace resource_mangment.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly Database_ctx _ctx;
        private readonly ITokenGenerator _tokenGeneration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthServiceHandlingRegisterErrors _registerErrors;
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthServiceLoginHandling _loginErrors;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            Database_ctx ctx,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGeneration,
            IAuthServiceHandlingRegisterErrors registerErrors,
            ILogger<AuthService> logger,
            IMapper mapper,
            IAuthServiceLoginHandling loginErrors,
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _passwordHasher = passwordHasher;
            _tokenGeneration = tokenGeneration;
            _ctx = ctx;
            _registerErrors = registerErrors;
            _logger = logger;
            _mapper = mapper;
            _loginErrors = loginErrors;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult<ServerStatusResponse>> RegisterCompanyAndOwnerAsync(
            RegisterCompanyAndOwner registerCompany
        )
        {
            using var transaction = await _ctx.Database.BeginTransactionAsync();
            try
            {
                var Validate = await _registerErrors.ValidateRegisterCompanyAndUserEndpoint(
                    registerCompany
                );

                if (Validate.Success == false)
                {
                    return Validate;
                }

                var createCompany = _mapper.Map<Company>(registerCompany);
                var createOwner = _mapper.Map<Employee>(registerCompany);
                var dtoToGenerateOwner = _mapper.Map<JwtGenerationAccountModel>(registerCompany);

                createOwner.CompanyID = createCompany.Id;
                createCompany.Name = registerCompany.CompanyName;
                createOwner.PasswordHash = _passwordHasher.Hash(registerCompany.Password);
                dtoToGenerateOwner.Id = createOwner.Id;

                await _ctx.Companies.AddAsync(createCompany);
                await _ctx.Employees.AddAsync(createOwner);
                await _ctx.SaveChangesAsync();

                await _userManager.AddToRoleAsync(createOwner, "Owner");
                await _ctx.SaveChangesAsync();

                var generateTokenAfterRegisteringCompany = _tokenGeneration.GenerateToken(
                    dtoToGenerateOwner
                );

                await transaction.CommitAsync();
                return new ServerStatusResponse
                {
                    Message = "Firma i właściciel zostali pomyślnie utworzeni, rola przypisana.",
                    Success = true,
                    Title = "Sukces",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.Created,
                    Token = generateTokenAfterRegisteringCompany,
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Wystąpił błąd podczas rejestracji firmy i właściciela.");
                return new ServerStatusResponse
                {
                    Message = "Wystąpił błąd podczas rejestracji.",
                    Success = false,
                    Title = "Błąd rejestracji",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError,
                    Token = null,
                };
            }
        }

        public async Task<ActionResult<ServerStatusResponse>> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var validate = await _loginErrors.ValidateLoginEndpoint(loginRequest);
                if (validate.Success == false)
                {
                    return validate;
                }

                var findUser = await _ctx.Employees.FirstOrDefaultAsync(employee =>
                    employee.Email == loginRequest.Email
                );

                bool checkIfPasswordMatch = _passwordHasher.VerifyPassword(
                    loginRequest.Password,
                    findUser.PasswordHash
                );

                if (!checkIfPasswordMatch)
                {
                    return new ServerStatusResponse
                    {
                        Message = "Podane hasło jest nieprawidłowe.",
                        Success = false,
                        Title = "Podane hasło jest nieprawidłowe.",
                        CurrentStatus = Status.Finished.ToString(),
                        StatusCode = HttpStatusCode.Forbidden,
                        Token = null,
                    };
                }

                var tokenDataForUser = _mapper.Map<JwtGenerationAccountModel>(findUser);

                var token = _tokenGeneration.GenerateToken(tokenDataForUser);

                return new ServerStatusResponse
                {
                    Message = "Logowanie przebiegło pomyślnie.",
                    Success = true,
                    Title = "Logowanie przebiegło pomyślnie.",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.OK,
                    Token = token,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult<ServerStatusResponse>> RegisterEmployeeAsync(
            RegisterEmployee registerEmployee
        )
        {
            try
            {
                return new ServerStatusResponse
                {
                    Message = "Użytkownik został pomyślnie założony.",
                    Success = true,
                    Title = "Użytkownik został pomyślnie założony.",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.Created,
                    Token = null,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
