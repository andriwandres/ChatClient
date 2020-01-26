using ChatClient.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IMessageRecipientRepository : IRepository<MessageRecipient>
    {
        Task<IEnumerable<MessageRecipient>> GetLatestMessages(int userId);
    }
}
