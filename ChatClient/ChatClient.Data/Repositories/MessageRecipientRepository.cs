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

        public async Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientUserId)
        {
            IEnumerable<MessageRecipient> messages = await Context.MessageRecipients
                .Include(mr => mr.Message)
                .ThenInclude(m => m.Author)
                .Where(mr =>
                    (mr.Message.AuthorId == userId && mr.RecipientUserId == recipientUserId) ||
                    (mr.Message.AuthorId == recipientUserId && mr.RecipientUserId == userId)
                )
                .ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId)
        {
            IEnumerable<MessageRecipient> messages = await Context.MessageRecipients
                .Include(mr => mr.RecipientGroup)
                .Where(mr => 
                    mr.RecipientGroup != null && 
                    mr.RecipientGroup.GroupId == groupId && 
                    mr.RecipientGroup.UserId == userId
                )
                .ToListAsync();

            return messages;
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
            IEnumerable<MessageRecipient> latestAuthoredMessages = Context.MessageRecipients
                .Include(mr => mr.RecipientGroup)
                .Include(mr => mr.Message)
                .ThenInclude(m => m.Author)
                .Where(mr => mr.Message.AuthorId == user.UserId)
                .ToList()
                .GroupBy(mr => new {
                    UserId = mr.RecipientUserId == user.UserId
                        ? mr.Message.AuthorId
                        : mr.RecipientUserId,
                    GroupId = mr.RecipientGroup == null
                        ? null
                        : (int?) mr.RecipientGroup.GroupId
                })
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First());

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
    }
}
