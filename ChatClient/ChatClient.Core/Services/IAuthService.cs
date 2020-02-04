using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.Auth;
using ChatClient.Core.Models.ViewModels.Auth;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IAuthService
    {
        Task<AuthenticatedUser> Authenticate();
        Task<User> GetUser();
        Task<bool> IsEmailTaken(string email);
        Task<AuthenticatedUser> Login(LoginDto credentials);
        Task Register(RegisterDto credentials);
    }
}
