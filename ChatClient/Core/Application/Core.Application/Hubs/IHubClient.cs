using Core.Domain.Dtos.Messages;
using System.Threading.Tasks;

namespace Core.Application.Hubs;

public interface IHubClient
{
    Task ReceiveMessage(ReceiveMessagePayload payload);
}