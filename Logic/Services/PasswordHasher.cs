using System.Security.Cryptography;

namespace resource_mangment.Logic.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 50000;

        private readonly HashAlgorithmName Alogorith = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                Alogorith,
                HashSize
            );

            return $"{Convert.ToHexString(hash)} - {Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                Alogorith,
                HashSize
            );

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
