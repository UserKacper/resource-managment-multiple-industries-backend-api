using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.ServerResponseModel;

namespace resource_mangment.Logic.Services.ASRegisterErrorHandling
{
    public interface IAuthServiceHandlingRegisterErrors
    {
        Task<ServerStatusResponse> ValidateRegisterCompanyAndUserEndpoint(
            RegisterCompanyAndOwner registerCompanyAndOwner
        );
    }
}
