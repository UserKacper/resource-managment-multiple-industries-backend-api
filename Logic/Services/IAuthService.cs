using Microsoft.AspNetCore.Mvc;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.ServerResponseModel;

namespace resource_mangment.Logic.Services
{
    public interface IAuthService
    {
        Task<ActionResult<ServerStatusResponse>> LoginAsync(LoginRequest loginRequest);
        Task<ActionResult<ServerStatusResponse>> RegisterCompanyAndOwnerAsync(
            RegisterCompanyAndOwner registerCompany
        );
        Task<ActionResult<ServerStatusResponse>> RegisterEmployeeAsync(
            RegisterEmployee registerEmployee
        );
    }
}
