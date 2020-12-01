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
    public class RecipientRepository : RepositoryBase, IRecipientRepository
    {
        public RecipientRepository(IChatContext context) : base(context)
        {
        }

        public IQueryable<Recipient> GetById(int recipientId)
        {
            return Context.Recipients
                .AsNoTracking()
                .Where(recipient => recipient.RecipientId == recipientId);
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
}
