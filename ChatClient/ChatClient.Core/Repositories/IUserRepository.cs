using ChatClient.Core.Models.Domain;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserCodeExists(string code);
        Task<bool> IsEmailTaken(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByCode(string code);
        Task<User> GetUserById(int userId);
        Task AddUser(User user);
    }
}
