using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class MessageRecipientRepository : Repository<MessageRecipient>, IMessageRecipientRepository
    {
        public MessageRecipientRepository(ChatContext context) : base(context) { }

        public async Task<IEnumerable<MessageRecipient>> GetLatestMessages(int userId)
        {
            User user = await Context.Users
                .Include(u => u.ReceivedPrivateMessages)
                    .ThenInclude(mr => mr.Message)

                .Include(u => u.GroupMemberships)
                    .ThenInclude(gm => gm.ReceivedGroupMessages)
                        .ThenInclude(gm => gm.Message)

                .SingleOrDefaultAsync(u => u.UserId == userId);

            IEnumerable<MessageRecipient> latestAuthoredMessages = Context.MessageRecipients
                .Include(mr => mr.Message)
                .Where(mr => mr.Message.AuthorId == user.UserId)
                .GroupBy(mr => new { mr.RecipientUserId, mr.RecipientGroupId })
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First());

            IEnumerable<MessageRecipient> latestReceivedPrivateMessages = user.ReceivedPrivateMessages
                .GroupBy(mr => mr.RecipientUserId)
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First());

            IEnumerable<MessageRecipient> latestReceivedGroupMessages = user.GroupMemberships
                .Select(gm => gm.ReceivedGroupMessages.OrderByDescending(mr => mr.Message.CreatedAt).First());

            IEnumerable<MessageRecipient> latestMessages = latestAuthoredMessages
                .Concat(latestReceivedPrivateMessages)
                .Concat(latestReceivedGroupMessages)
                .OrderByDescending(mr => mr.Message.CreatedAt)
                .GroupBy(mr => new { mr.RecipientUserId, mr.RecipientGroupId })
                .Select(grouping => grouping.OrderByDescending(mr => mr.Message.CreatedAt).First());

            // TODO try GroupJoin() instead of Concat() + GroupBy()

            return latestMessages;
        }
    }
}
