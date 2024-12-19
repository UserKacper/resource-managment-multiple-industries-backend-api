using resource_mangment.Logic.DTO_s;

namespace resource_mangment.Logic.TokenService
{
    public interface ITokenGenerator
    {
        string GenerateToken(JwtGenerationAccountModel employee);
    }
}
