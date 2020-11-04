using Core.Domain.Entities;

namespace Core.Application.Services
{
    public interface ICryptoService
    {
        string GenerateToken(User user);
        byte[] GenerateSalt();
        byte[] HashPassword(string password, byte[] salt);
        bool VerifyPassword(byte[] hashed, byte[] salt, string password);
    }
}
