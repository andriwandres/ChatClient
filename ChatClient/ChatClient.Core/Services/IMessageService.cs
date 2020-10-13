using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<LatestMessageViewModel>> GetLatestMessages();
        Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId);
        Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int targetUserId);
        Task AddGroupMessage(int groupId, string message);
        Task AddPrivateMessage(int targetUserId, string message);
    }
}
