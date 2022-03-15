using Microsoft.AspNetCore.SignalR;

namespace Core.Application.Hubs;

public abstract class ChatHubBase : Hub<IHubClient>
{
}