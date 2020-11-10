using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IFriendshipRepository
    {
        IQueryable<Friendship> GetById(int friendshipId);
        Task<bool> Exists(int friendshipId, CancellationToken cancellationToken = default);
        Task Add(Friendship friendship, CancellationToken cancellationToken = default);
        void Remove(Friendship friendship);
    }
}
