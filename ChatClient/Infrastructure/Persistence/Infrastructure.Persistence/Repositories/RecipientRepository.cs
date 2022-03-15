using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class RecipientRepository : RepositoryBase<Recipient>, IRecipientRepository
{
    public RecipientRepository(IChatContext context) : base(context)
    {
    }

    public async Task<Recipient> GetByIdIncludingMemberships(int recipientId, CancellationToken cancellationToken = default)
    {
        return await Context.Recipients
            .AsTracking()
            .Include(r => r.GroupMembership)
            .ThenInclude(gm => gm.Group)
            .ThenInclude(g => g.Memberships)
            .ThenInclude(gm => gm.Recipient)
            .SingleOrDefaultAsync(gm => gm.RecipientId == recipientId, cancellationToken);
    }

    public async Task<bool> Exists(int recipientId, CancellationToken cancellationToken = default)
    {
        return await Context.Recipients
            .AsNoTracking()
            .AnyAsync(recipient => recipient.RecipientId == recipientId, cancellationToken);
    }

    public async Task Add(Recipient recipient, CancellationToken cancellationToken = default)
    {
        await Context.Recipients.AddAsync(recipient, cancellationToken);
    }
}