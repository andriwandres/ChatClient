using Core.Application.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using MediatR;

namespace Infrastructure.Socket.Hubs
{
    public class ChatHub : Hub<IHubClient>
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
