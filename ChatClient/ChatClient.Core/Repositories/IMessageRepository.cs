using ChatClient.Core.Models.Domain;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> GetMessageById(int messageId);
        Task AddMessage(Message message);
        void EditMessage(Message message);
    }
}
