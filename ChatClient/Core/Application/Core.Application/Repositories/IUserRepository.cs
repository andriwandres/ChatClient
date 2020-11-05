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
        Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);
        Task<bool> UserNameExists(string userName, CancellationToken cancellationToken = default);
    }
}
