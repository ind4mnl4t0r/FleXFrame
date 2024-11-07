using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.UtilityHub
{
    public class PasswordEngine
    {
        private const int SaltSize = 16; // 128-bit (16 byte) salt; for making the hash unique and prevents from rainbow-table attack
        private const int HashSize = 32; // 256-bit (32 byte) hash; for making the hash larger and prevents from brute-force attack
        private const int Iterations = 100_000; // (100,000) Recommended minimum for PBKDF2; higher means safer, but slower

        public static byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        public static byte[] HashPassword(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize
            );
        }

        public static bool VerifyPassword(string password, byte[] salt, byte[] hash)
        {
            var hashedInput = HashPassword(password, salt);
            return CryptographicOperations.FixedTimeEquals(hashedInput, hash);
        }
    }
}
