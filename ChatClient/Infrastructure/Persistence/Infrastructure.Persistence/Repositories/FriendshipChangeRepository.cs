using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class FriendshipChangeRepository : RepositoryBase<FriendshipChange>, IFriendshipChangeRepository
{
    public FriendshipChangeRepository(IChatContext context) : base(context)
    {
    }

    public async Task Add(FriendshipChange change, CancellationToken cancellationToken = default)
    {
        await Context.FriendshipChanges.AddAsync(change, cancellationToken);
    }

    public async Task<List<FriendshipChange>> GetByFriendship(int friendshipId)
    {
        return await Context.FriendshipChanges
            .AsNoTracking()
            .Where(change => change.FriendshipId == friendshipId)
            .ToListAsync();
    }
}