using Microsoft.EntityFrameworkCore;
using resource_manager_db.Db_connector;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.ServerResponseModel;

namespace resource_mangment.Logic.Services.ASRegisterErrorHandling
{
    public class AuthServiceHandlingRegisterErrors : IAuthServiceHandlingRegisterErrors
    {
        private readonly Database_ctx _ctx;

        public AuthServiceHandlingRegisterErrors(Database_ctx ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServerStatusResponse> ValidateRegisterCompanyAndUserEndpoint(
            RegisterCompanyAndOwner registerCompanyAndOwner
        )
        {
            try
            {
                if (await CheckICompanyAlreadyExist(registerCompanyAndOwner.NIP))
                {
                    return new ServerStatusResponse
                    {
                        Message = "Firma z takim NIPem juz istnieje.",
                        Success = false,
                        Title = "Firma z podanym Nipem juz jest zarejestrowana",
                        CurrentStatus = Status.Finished.ToString(),
                        StatusCode = System.Net.HttpStatusCode.Conflict,
                    };
                }
                if (await CheckIfOwnerEmailIsAlreadyRegistered(registerCompanyAndOwner.Email))
                {
                    return new ServerStatusResponse
                    {
                        Message = "Podany Email jest juz zajęty.",
                        Success = false,
                        Title = "Firma z podanym Nipem juz jest zarejestrowana",
                        CurrentStatus = Status.Finished.ToString(),
                        StatusCode = System.Net.HttpStatusCode.Conflict,
                    };
                }

                return new ServerStatusResponse
                {
                    Message = "Pomyślna Walidacja",
                    Success = true,
                    Title = "Pomyślna Walidacja",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<bool> CheckICompanyAlreadyExist(string NIP)
        {
            try
            {
                bool isCompany = await _ctx.Companies.AnyAsync(company => company.NIP == NIP);

                if (isCompany)
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<bool> CheckIfOwnerEmailIsAlreadyRegistered(string Email)
        {
            try
            {
                bool isOwnerRegistered = await _ctx.Employees.AnyAsync(employee =>
                    employee.Email == Email
                );

                if (isOwnerRegistered)
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
