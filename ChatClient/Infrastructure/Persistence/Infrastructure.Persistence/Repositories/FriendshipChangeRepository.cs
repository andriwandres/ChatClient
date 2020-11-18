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
    public class FriendshipChangeRepository : RepositoryBase, IFriendshipChangeRepository
    {
        public FriendshipChangeRepository(IChatContext context) : base(context)
        {
        }

        public async Task Add(FriendshipChange change, CancellationToken cancellationToken = default)
        {
            await Context.FriendshipChanges.AddAsync(change, cancellationToken);
        }

        public IQueryable<FriendshipChange> GetByFriendship(int friendshipId)
        {
            return Context.FriendshipChanges
                .AsNoTracking()
                .Where(change => change.FriendshipId == friendshipId);
        }
    }
}
