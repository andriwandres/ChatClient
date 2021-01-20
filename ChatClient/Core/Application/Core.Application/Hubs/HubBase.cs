using Microsoft.AspNetCore.SignalR;

namespace Core.Application.Hubs
{
    public abstract class HubBase : Hub<IHubClient>
    {
    }
}
