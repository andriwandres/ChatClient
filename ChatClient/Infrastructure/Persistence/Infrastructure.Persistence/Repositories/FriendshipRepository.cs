using System.Collections.Generic;
using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FriendshipRepository : RepositoryBase<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(IChatContext context) : base(context)
        {

        }

        public async Task<List<Friendship>> GetByUser(int userId)
        {
            return await Context.Friendships
                .AsNoTracking()
                .Where(friendship => friendship.RequesterId == userId || friendship.AddresseeId == userId)
                .ToListAsync();
        }

        public async Task<bool> Exists(int friendshipId, CancellationToken cancellationToken = default)
        {
            return await Context.Friendships
                .AsNoTracking()
                .AnyAsync(friendship => friendship.FriendshipId == friendshipId, cancellationToken);
        }

        public async Task<bool> CombinationExists(int requesterId, int addresseeId, CancellationToken cancellationToken = default)
        {
            return await Context.Friendships
                .AsNoTracking()
                .AnyAsync(friendship =>
                    (friendship.RequesterId == requesterId && friendship.AddresseeId == addresseeId) ||
                    (friendship.AddresseeId == requesterId && friendship.RequesterId == addresseeId), 
                    cancellationToken
                );
        }

        public async Task Add(Friendship friendship, CancellationToken cancellationToken = default)
        {
            await Context.Friendships.AddAsync(friendship, cancellationToken);
        }
    }
}
