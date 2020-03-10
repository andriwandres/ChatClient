using ChatClient.Core.Models.Domain;
using System.Threading.Tasks;

namespace ChatClient.Core.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
    }
}
