using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class RecipientRepository : RepositoryBase, IRecipientRepository
    {
        public RecipientRepository(IChatContext context) : base(context)
        {
        }

        public async Task Add(Recipient recipient, CancellationToken cancellationToken = default)
        {
            await Context.Recipients.AddAsync(recipient, cancellationToken);
        }
    }
}
