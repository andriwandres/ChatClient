using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.Auth;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IAuthService
    {
        Task<User> GetCurrentUser();
        Task<bool> EmailExists(string email);
        Task<(User user, string token)> Authenticate();
        Task<(User user, string token)> Login(LoginDto credentials);
        Task Register(RegisterDto credentials);
    }
}
