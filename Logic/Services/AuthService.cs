using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resource_manager_db.Db_connector;
using resource_manager_db.Models;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.DTO_s;
using resource_mangment.Logic.ServerResponseModel;
using resource_mangment.Logic.Services.ASRegisterErrorHandling;
using resource_mangment.Logic.TokenService;

namespace resource_mangment.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private Database_ctx _ctx;
        private readonly ITokenGenerator _tokenGeneration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthServiceHandlingRegisterErrors _registerErrors;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            Database_ctx ctx,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGeneration,
            IAuthServiceHandlingRegisterErrors registerErrors,
            ILogger<AuthService> logger,
            IMapper mapper
        )
        {
            _passwordHasher = passwordHasher;
            _tokenGeneration = tokenGeneration;
            _ctx = ctx;
            _registerErrors = registerErrors;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActionResult<ServerStatusResponse>> RegisterCompanyAndOwnerAsync(
            RegisterCompanyAndOwner registerCompany
        )
        {
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

                await _ctx.Companies.AddAsync(createCompany);
                await _ctx.Employees.AddAsync(createOwner);
                await _ctx.SaveChangesAsync();

                var generateTokenAfterRegisteringCompany = _tokenGeneration.GenerateToken(
                    dtoToGenerateOwner
                );

                return new ServerStatusResponse
                {
                    Message = "Firma została pomyślnie założona",
                    Success = true,
                    Title = "Firma została pomyślnie założona",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.Created,
                    Token = generateTokenAfterRegisteringCompany,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult<ServerStatusResponse>> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                return new ServerStatusResponse
                {
                    Message = "Logowanie przebiegło pomyślnie.",
                    Success = true,
                    Title = "Logowanie przebiegło pomyślnie.",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.OK,
                    Token = null,
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
                    Title = "Firma została pomyślnie założona",
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
