namespace resource_mangment.Logic.Services
{
    public interface IPasswordHasher
    {
        string Hash (string password);
        bool VerifyPassword (string password, string passwordHash);
    }
}