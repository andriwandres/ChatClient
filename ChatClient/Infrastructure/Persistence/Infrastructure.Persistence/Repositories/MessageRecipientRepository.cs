using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class MessageRecipientRepository : RepositoryBase, IMessageRecipientRepository
    {
        public MessageRecipientRepository(IChatContext context) : base(context)
        {

        }

        public IQueryable<MessageRecipient> GetLatestGroupedByRecipients(int userId)
        {
            IQueryable<MessageRecipient> latestMessages = Context.MessageRecipients
                .AsNoTracking()
                .Where(mr =>
                    mr.Recipient.UserId == userId ||
                    mr.Recipient.UserId == null &&
                    mr.Recipient.GroupMembership.UserId == userId
                )
                .GroupByTargetAndGetLatest(userId)
                .OrderByDescending(mr => mr.Message.Created);

            return latestMessages;
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
