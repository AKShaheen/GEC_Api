using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GEC.Business.Services.PasswordHash
{
    public static class PasswordHasher 
    {
        private const int _SaltSize = 128 / 8;
        private const int _KeySize = 256 / 8;
        private const int _Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

        public static (string PasswordHash, string PasswordSalt) Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(_SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _Iterations, _hashAlgorithmName, _KeySize);

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public static bool VerifyPassword(string passwordHash,string passwordSalt, string inputPassword)
        {
            var salt = Convert.FromBase64String(passwordSalt);
            var hash = Convert.FromBase64String(passwordHash);

            var inputPasswordHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, _Iterations, _hashAlgorithmName, _KeySize); 

            return CryptographicOperations.FixedTimeEquals(hash, inputPasswordHash);
        }
    }
}