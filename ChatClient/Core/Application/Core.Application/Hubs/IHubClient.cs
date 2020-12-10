using System.Threading.Tasks;

namespace Core.Application.Hubs
{
    public interface IHubClient
    {
        Task ReceiveMessage(string payload);
    }
}