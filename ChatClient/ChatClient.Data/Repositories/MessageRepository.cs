using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class MessageRepository : Repository<ChatContext>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context) { }

        public Task AddMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}
