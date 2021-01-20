using Core.Application.Hubs;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Socket.Hubs
{
    public class ChatHub : HubBase
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
