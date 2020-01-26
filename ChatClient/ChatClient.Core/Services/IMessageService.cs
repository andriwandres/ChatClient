using ChatClient.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Core.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageRecipient>> GetLatestMessages();
    }
}
