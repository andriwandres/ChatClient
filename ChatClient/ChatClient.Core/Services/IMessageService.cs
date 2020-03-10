using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageRecipient>> GetLatestMessages();
        Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId);
        Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId);
        Task AddGroupMessage(int groupId, string message);
        Task AddPrivateMessage(int recipientId, string message);
    }
}
