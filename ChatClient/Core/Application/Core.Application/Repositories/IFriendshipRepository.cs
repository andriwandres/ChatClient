using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IFriendshipRepository
    {
        IQueryable<Friendship> GetById(int friendshipId);
        IQueryable<Friendship> GetByUser(int userId);
        Task<bool> Exists(int friendshipId, CancellationToken cancellationToken = default);
        Task Add(Friendship friendship, CancellationToken cancellationToken = default);
    }
}
