using ChatClient.Core.Models;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Data.Repositories
{
    public class MessageRecipientRepository : Repository<MessageRecipient>, IMessageRecipientRepository
    {
        public MessageRecipientRepository(ChatContext context) : base(context) { }
    }
}
