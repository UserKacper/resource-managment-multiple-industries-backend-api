using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.ServerResponseModel;
using resource_mangment.Logic.Services;

namespace resource_mangment.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<ServerStatusResponse> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                return new ServerStatusResponse
                {
                    Message = "Firma zosta³a pomyœlnie za³o¿ona",
                    Success = false,
                    Title = "Firma zosta³a pomyœlnie za³o¿ona",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.Conflict,
                    Token = "",
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Register/Company")]
        public async Task<ServerStatusResponse> RegisterCompany(
            [FromBody] RegisterCompanyAndOwner registerCompanyAndOwner
        )
        {
            try
            {
                var registerCompany = await _authService.RegisterCompanyAndOwnerAsync(
                    registerCompanyAndOwner
                );

                return new ServerStatusResponse
                {
                    Message = registerCompany.Value.Message,
                    Success = registerCompany.Value.Success,
                    Title = registerCompany.Value.Title,
                    CurrentStatus = registerCompany.Value.CurrentStatus,
                    StatusCode = registerCompany.Value.StatusCode,
                    Token = registerCompany.Value.Token,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("register/employee")]
        public async Task<ServerStatusResponse> RegisterEmployee(
            [FromBody] RegisterEmployee registerEmployeeRequest
        )
        {
            try
            {
                return new ServerStatusResponse
                {
                    Message = "Firma zosta³a pomyœlnie za³o¿ona",
                    Success = false,
                    Title = "Firma zosta³a pomyœlnie za³o¿ona",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.Conflict,
                    Token = "",
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
