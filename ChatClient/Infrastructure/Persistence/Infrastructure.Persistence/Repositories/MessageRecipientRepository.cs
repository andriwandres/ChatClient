using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Dtos.Messages;
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

        public IQueryable<MessageRecipient> GetMessagesWithRecipient(int userId, int recipientId, MessageBoundaries boundaries)
        {
            IQueryable<MessageRecipient> messages = Context.MessageRecipients
                .AsNoTracking()
                .Where(mr =>
                    (boundaries.Before == null || mr.Message.Created < boundaries.Before) &&
                    (boundaries.After == null || mr.Message.Created > boundaries.After)
                )
                .Where(mr =>
                    mr.RecipientId == recipientId && mr.Message.AuthorId == userId ||
                    mr.Recipient.GroupMembership.Recipient.RecipientId == recipientId ||
                    mr.Recipient.UserId == userId && mr.Message.Author.Recipient.RecipientId == recipientId
                )
                .OrderBy(mr => mr.Message.Created);

            // Limit messages from bottom up
            if (boundaries.Limit != null)
            {
                return messages
                    .OrderByDescending(mr => mr.Message.Created)
                    .Take((int) boundaries.Limit)
                    .OrderBy(mr => mr.Message.Created);
            }

            return messages;
        }

        public IQueryable<MessageRecipient> GetLatestGroupedByRecipients(int userId)
        {
            IQueryable<MessageRecipient> latestMessages = Context.MessageRecipients
                .AsNoTracking()
                .Where(mr =>
                    mr.Message.AuthorId == userId &&
                    mr.Recipient.GroupMembershipId == null ||

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
