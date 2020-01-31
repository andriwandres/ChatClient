using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Data.Repositories
{
    public class MessageRepository : Repository<ChatContext>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context) { }
    }
}
