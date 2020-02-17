using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IMessageRecipientRepository
    {
        Task<IEnumerable<MessageRecipient>> GetLatestMessages(int userId);
        Task<IEnumerable<MessageRecipient>> GetGroupMessages(int userId, int groupId);
        Task<IEnumerable<MessageRecipient>> GetPrivateMessages(int userId, int recipientId);
    }
}
