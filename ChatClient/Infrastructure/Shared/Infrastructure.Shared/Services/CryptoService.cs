using Core.Application.Services;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Shared.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IDateProvider _dateProvider;

        public CryptoService(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public string GenerateToken(User user, string secret)
        {
            byte[] key = Encoding.ASCII.GetBytes(secret);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                }),
                Expires = _dateProvider.UtcNow().AddHours(7),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];

            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(salt);

            return salt;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10_000,
                numBytesRequested: 32
            );
        }

        public bool VerifyPassword(byte[] hashed, byte[] salt, string password)
        {
            return hashed.SequenceEqual(HashPassword(password, salt));
        }
    }
}
