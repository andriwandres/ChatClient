using Core.Application.Common;
using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByIdIncludingRecipient(int userId, CancellationToken cancellationToken = default);
    Task<User> GetByUserNameOrEmail(string userNameOrEmail);
    Task<bool> Exists(int userId, CancellationToken cancellationToken = default);
    Task<bool> UserNameOrEmailExists(string userName, string email, CancellationToken cancellationToken = default);
    Task Add(User user, CancellationToken cancellationToken = default);
}