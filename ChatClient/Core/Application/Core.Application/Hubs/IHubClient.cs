using Core.Domain.Resources.Messages;
using System.Threading.Tasks;

namespace Core.Application.Hubs
{
    public interface IHubClient
    {
        Task ReceiveMessage(int recipientId, ChatMessageResource message);
    }
}
