using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageRecipient>> GetLatestMessages();
    }
}
