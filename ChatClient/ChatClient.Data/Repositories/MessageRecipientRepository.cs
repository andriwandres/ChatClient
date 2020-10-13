using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using ChatClient.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class MessageRecipientRepository : Repository<ChatContext>, IMessageRecipientRepository
    {
        public MessageRecipientRepository(ChatContext context) : base(context) { }

        public async Task<IEnumerable<LatestMessageViewModel>> GetLatestMessages(int userId)
        {
            IQueryable<LatestMessageViewModel> latestMessages = Context.MessageRecipients
                .AsNoTracking()
                .Where(mr =>
                    (mr.RecipientGroupId != null && mr.RecipientGroup.UserId == userId) ||
                    (mr.RecipientUserId != null && (mr.RecipientUserId == userId || mr.Message.AuthorId == userId))
                )
                .GroupByTargetAndGetLatestMessages(userId)
                .OrderByDescending(mr => mr.Message.CreatedAt)
                .Select(mr => new LatestMessageViewModel
                {
                    AuthorId = mr.Message.AuthorId,
                    AuthorName = mr.Message.Author.DisplayName,
                    CreatedAt = mr.Message.CreatedAt,
                    IsRead = mr.IsRead,
                    MessageId = mr.MessageId,
                    MessageRecipientId = mr.MessageRecipientId,
                    TextContent = mr.Message.TextContent,
                    UnreadMessagesCount = mr.RecipientGroupId == null
                        ? mr.RecipientUser.ReceivedPrivateMessages.Count(m => m.IsRead == false)
                        : mr.RecipientGroup.ReceivedGroupMessages.Count(m => m.IsRead == false && mr.RecipientGroup.UserId != userId),
                    RecipientGroup = mr.RecipientGroupId == null ? null : new RecipientGroupViewModel
                    {
                        GroupId = mr.RecipientGroup.GroupId,
                        Name = mr.RecipientGroup.Group.Name,
                    },
                    RecipientUser = mr.RecipientUserId == null ? null : new RecipientUserViewModel
                    {
                        UserId = mr.RecipientUserId == userId
                            ? mr.Message.Author.UserId
                            : mr.RecipientUser.UserId,

                        DisplayName = mr.RecipientUserId == userId
                            ? mr.Message.Author.DisplayName
                            : mr.RecipientUser.DisplayName,
                    }
                });

            return await latestMessages.ToListAsync();
        }

        public Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId)
        {
            throw new System.NotImplementedException();
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
