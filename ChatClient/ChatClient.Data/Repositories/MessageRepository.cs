using ChatClient.Core.Repositories;
using ChatClient.Data.Database;

namespace ChatClient.Data.Repositories
{
    public class MessageRepository : Repository<ChatContext>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context) { }
    }
}
