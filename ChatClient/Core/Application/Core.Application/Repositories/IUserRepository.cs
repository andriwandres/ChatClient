using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetById(int userId);
        IQueryable<User> GetByUserNameOrEmail(string userNameOrEmail);
        Task<bool> Exists(int userId, CancellationToken cancellationToken = default);
        Task<bool> UserNameOrEmailExists(string userName, string email, CancellationToken cancellationToken = default);
        Task Add(User user, CancellationToken cancellationToken = default);
    }
}
