using Core.Application.Hubs;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Socket.Hubs;

public class ChatHub : ChatHubBase
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}