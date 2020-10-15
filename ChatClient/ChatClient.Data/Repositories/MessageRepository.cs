using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatClient.Data.Repositories
{
    public class MessageRepository : Repository<ChatContext>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context) { }

        public async Task AddMessage(Message message)
        {
            await Context.Messages.AddAsync(message);
        }

        public void EditMessage(Message message)
        {
            Context.Messages.Update(message);
        }

        public async Task<Message> GetMessageById(int messageId)
        {
            return await Context.Messages
                .FirstOrDefaultAsync(m => m.MessageId == messageId);
        }
    }
}
