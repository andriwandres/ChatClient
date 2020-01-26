
using ChatClient.Core.Models.Domain;

namespace ChatClient.Core.Services
{
    public interface ICryptoService
    {
        string GenerateUserCode();
        string GenerateToken(User user, string secret);
        byte[] GenerateSalt();
        byte[] HashPassword(string password, byte[] salt);
        bool VerifyPassword(byte[] hash, byte[] salt, string password);
    }
}
