using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IMessageRecipientRepository
    {
        Task<IEnumerable<LatestMessageViewModel>> GetLatestMessages(int userId);
        Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId);
        Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId);
        Task AddGroupMessage(IEnumerable<MessageRecipient> recipients);
        Task AddPrivateMessage(MessageRecipient recipient);
    }
}
