using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using resource_mangment.Logic.DTO_s;

namespace resource_mangment.Logic.TokenService
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(JwtGenerationAccountModel employee)
        {
            var data = Encoding.UTF8.GetBytes("SomeStringFromConfig1234 SomeStringFromConfig1234");
            var securityKey = new SymmetricSecurityKey(data);

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.NameIdentifier] = employee.Id,
                [ClaimTypes.Email] = employee.Email,
                [ClaimTypes.Role] = employee.Role,
                [ClaimTypes.Expiration] = DateTime.UtcNow.AddMinutes(120),
                [ClaimTypes.Name] = employee.Name,
                [ClaimTypes.MobilePhone] = employee.MobilePhone,
            };
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration[""],
                Audience = _configuration[""],
                Claims = claims,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

            var handler = new JsonWebTokenHandler();
            handler.SetDefaultTimesOnTokenCreation = false;
            var tokenString = handler.CreateToken(descriptor);
            return tokenString;
        }
    }
}
