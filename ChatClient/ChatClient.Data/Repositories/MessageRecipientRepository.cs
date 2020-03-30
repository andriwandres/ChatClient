using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class MessageRecipientRepository : Repository<ChatContext>, IMessageRecipientRepository
    {
        public MessageRecipientRepository(ChatContext context) : base(context) { }

        public async Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId)
        {
            IEnumerable<MessageRecipient> messages = await Context.MessageRecipients
                .Include(mr => mr.Message)
                .ThenInclude(m => m.Author)
                .Where(mr =>
                    (mr.Message.AuthorId == userId && mr.RecipientUserId == recipientId) ||
                    (mr.Message.AuthorId == recipientId && mr.RecipientUserId == userId)
                )
                .ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId)
        {
            // Get Group Messages that the user has received
            IEnumerable<MessageRecipient> receivedMessages = await Context.MessageRecipients
                .Include(mr => mr.RecipientGroup)
                .Include(mr => mr.Message)
                .ThenInclude(m => m.Author)
                .Where(mr =>
                    mr.RecipientGroup != null &&
                    mr.RecipientGroup.GroupId == groupId &&
                    (mr.RecipientGroup.UserId == userId)
                )
                .ToListAsync();

            // Get Group Messages Written by the User himself
            IEnumerable<MessageRecipient> authoredMessages = await Context.Messages
                .Include(m => m.Recipients)
                .ThenInclude(mr => mr.RecipientGroup)
                .Include(m => m.Recipients)
                .ThenInclude(mr => mr.Message)
                .ThenInclude(mr => mr.Author)
                .Where(m =>
                    m.AuthorId == userId &&
                    m.Recipients.First().RecipientGroup != null &&
                    m.Recipients.First().RecipientGroup.GroupId == groupId
                )
                .Select(m => m.Recipients.First())
                .ToListAsync();

            // Union the groups together
            IEnumerable<MessageRecipient> messages = receivedMessages.Concat(authoredMessages);

            return messages;
        }

        public async Task<IEnumerable<MessageRecipient>> GetLatestAuthoredMessages(int userId)
        {
            IEnumerable<MessageRecipient> allAuthoredMessages = await Context.MessageRecipients
                .Include(mr => mr.RecipientGroup)
                .Include(mr => mr.Message)
                    .ThenInclude(m => m.Author)
                .Where(mr => mr.Message.AuthorId == userId)
                .ToListAsync();

            IEnumerable<MessageRecipient> latestAuthoredMessages = allAuthoredMessages
                .GroupBy(mr => new
                {
                    UserId = mr.RecipientUserId == userId
                        ? mr.Message.AuthorId
                        : mr.RecipientUserId,

                    GroupId = mr.RecipientGroup == null
                        ? null
                        : (int?) mr.RecipientGroup.GroupId
                })
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First());

            return latestAuthoredMessages;
        }

        public async Task<IEnumerable<MessageRecipient>> GetLatestReceivedGroupMessages(int userId)
        {
            IEnumerable<GroupMembership> memberships = await Context.GroupMemberships
                .Include(gm => gm.ReceivedGroupMessages)
                .Where(gm => gm.UserId == userId)
                .ToListAsync();

            IEnumerable<MessageRecipient> latestReceivedGroupMessages = memberships
                .Select(gm => gm.ReceivedGroupMessages.OrderByDescending(mr => mr.Message.CreatedAt).First());

            return latestReceivedGroupMessages;
        }

        public async Task<IEnumerable<MessageRecipient>> GetLatestReceivedPrivateMessages(int userId)
        {
            return null;
        }

        public async Task<IEnumerable<MessageRecipient>> GetLatestMessages(int userId)
        {
            // Get the user with his related messages/groups
            User user = await Context.Users
                .Include(u => u.ReceivedPrivateMessages)
                    .ThenInclude(mr => mr.Message)
                        .ThenInclude(m => m.Author)

                .Include(u => u.GroupMemberships)
                    .ThenInclude(gm => gm.Group)

                .Include(u => u.GroupMemberships)
                    .ThenInclude(gm => gm.ReceivedGroupMessages)

                .Include(u => u.GroupMemberships)
                    .ThenInclude(gm => gm.ReceivedGroupMessages)
                        .ThenInclude(gm => gm.Message)
                            .ThenInclude(m => m.Author)

                .SingleOrDefaultAsync(u => u.UserId == userId);

            // Get latest messages from the user itself
            IEnumerable<MessageRecipient> latestAuthoredMessages = await GetLatestAuthoredMessages(userId);

            // Get latest messages that the user received through private chats
            IEnumerable<MessageRecipient> latestReceivedPrivateMessages = user.ReceivedPrivateMessages
                .GroupBy(mr => mr.Message.AuthorId)
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First())
                .AsEnumerable();
            
            // Get latest messages that the user received through group chats
            IEnumerable<MessageRecipient> latestReceivedGroupMessages = user.GroupMemberships
                .Select(gm => gm.ReceivedGroupMessages.OrderByDescending(mr => mr.Message.CreatedAt).First())
                .AsEnumerable();
            
            // Union messages together and group them
            IEnumerable<MessageRecipient> latestMessages = latestAuthoredMessages
                .Concat(latestReceivedPrivateMessages)
                .Concat(latestReceivedGroupMessages)
                .OrderByDescending(mr => mr.Message.CreatedAt)
                .GroupBy(mr => new
                {
                    UserId = mr.RecipientUserId == user.UserId
                        ? mr.Message.AuthorId
                        : mr.RecipientUserId,
                    GroupId = mr.RecipientGroup == null
                        ? null
                        : (int?)mr.RecipientGroup.GroupId
                })
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First());

            return latestMessages;
        }

        public async Task AddGroupMessage(IEnumerable<MessageRecipient> recipients)
        {
            await Context.MessageRecipients.AddRangeAsync(recipients);
        }

        public async Task AddPrivateMessage(MessageRecipient recipient)
        {
            await Context.MessageRecipients.AddAsync(recipient);
        }
    }
}
