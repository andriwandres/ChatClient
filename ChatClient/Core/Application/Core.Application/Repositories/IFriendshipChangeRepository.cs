using Core.Application.Common;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IFriendshipChangeRepository : IRepository<FriendshipChange>
    {
        Task Add(FriendshipChange change, CancellationToken cancellationToken = default);
        Task<List<FriendshipChange>> GetByFriendship(int friendshipId);
    }
}
