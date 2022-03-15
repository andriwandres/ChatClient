using Core.Application.Common;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Repositories;

public interface IFriendshipRepository : IRepository<Friendship>
{
    Task<List<Friendship>> GetByUser(int userId);
    Task<bool> Exists(int friendshipId, CancellationToken cancellationToken = default);
    Task<bool> CombinationExists(int requesterId, int addresseeId, CancellationToken cancellationToken = default);
    Task Add(Friendship friendship, CancellationToken cancellationToken = default);
}