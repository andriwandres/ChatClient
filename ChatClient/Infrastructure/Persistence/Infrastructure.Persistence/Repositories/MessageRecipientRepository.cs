using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class MessageRecipientRepository : RepositoryBase, IMessageRecipientRepository
    {
        public MessageRecipientRepository(IChatContext context) : base(context)
        {
        }

        public async Task Add(MessageRecipient messageRecipient, CancellationToken cancellationToken = default)
        {
            await Context.MessageRecipients.AddAsync(messageRecipient, cancellationToken);
        }

        public async Task AddRange(IEnumerable<MessageRecipient> messageRecipients, CancellationToken cancellationToken = default)
        {
            await Context.MessageRecipients.AddRangeAsync(messageRecipients, cancellationToken);
        }
    }
}
