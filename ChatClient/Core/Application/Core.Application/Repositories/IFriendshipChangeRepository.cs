using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IFriendshipChangeRepository
    {
        Task Add(FriendshipChange change, CancellationToken cancellationToken = default);
    }
}
