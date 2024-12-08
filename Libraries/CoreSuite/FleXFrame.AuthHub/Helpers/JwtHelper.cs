using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FleXFrame.AuthHub.Models;
using Microsoft.IdentityModel.Tokens;

namespace FleXFrame.AuthHub.Helpers
{
    public class JwtHelper
    {
        private readonly byte[] _secretKey;
        private readonly int _expiryInMinutes;

        public JwtHelper(string secretKey, int expiryInMinutes)
        {
            _secretKey = Encoding.UTF8.GetBytes(secretKey); // Convert the string key to a byte array using UTF8 encoding
            _expiryInMinutes = expiryInMinutes;
        }

        // Generate a JWT
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _secretKey; // Use the byte array key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserID)
            }),
                Expires = DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature) // Using HS256
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Validate a JWT
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _secretKey; // Use the byte array key that was already decoded

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // The same key used for signing
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
