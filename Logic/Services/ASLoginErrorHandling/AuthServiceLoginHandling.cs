using System.Net;
using Microsoft.EntityFrameworkCore;
using resource_manager_db.Db_connector;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.ServerResponseModel;

namespace resource_mangment.Logic.Services.ASLoginErrorHandling
{
    public class AuthServiceLoginHandling : IAuthServiceLoginHandling
    {
        private readonly Database_ctx _ctx;

        public AuthServiceLoginHandling(Database_ctx ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServerStatusResponse> ValidateLoginEndpoint(LoginRequest loginModel)
        {
            if (!await CheckIfUserExist(loginModel.Email))
            {
                return new ServerStatusResponse
                {
                    Message = "Właściciel firmu musi załozyć konto pracownikowi.",
                    Success = false,
                    Title = "Użytkownik nie istnieje.",
                    CurrentStatus = Status.Finished.ToString(),
                    StatusCode = HttpStatusCode.NotFound,
                };
            }
            return new ServerStatusResponse
            {
                Message = "Pomyślna Walidacja",
                Success = true,
                Title = "Pomyślna Walidacja",
                CurrentStatus = Status.Finished.ToString(),
                StatusCode = HttpStatusCode.OK,
            };
        }

        async Task<bool> CheckIfUserExist(string Email)
        {
            try
            {
                bool checkIfUserExist = await _ctx.Employees.AnyAsync(employee =>
                    employee.Email == Email
                );

                return checkIfUserExist;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
