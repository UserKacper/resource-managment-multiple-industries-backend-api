using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.ServerResponseModel;

namespace resource_mangment.Logic.Services.ASLoginErrorHandling
{
    public interface IAuthServiceLoginHandling
    {
        Task<ServerStatusResponse> ValidateLoginEndpoint(LoginRequest loginModel);
    }
}
