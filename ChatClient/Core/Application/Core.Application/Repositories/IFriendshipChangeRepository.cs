using Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IFriendshipChangeRepository
    {
        Task Add(FriendshipChange change, CancellationToken cancellationToken = default);
        IQueryable<FriendshipChange> GetByFriendship(int friendshipId);
    }
}
