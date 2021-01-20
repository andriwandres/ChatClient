using Core.Application.Hubs;
using Infrastructure.Socket.Hubs;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Socket.Extensions
{
    public static class DependencyInjection
    {
        public static void AddSocketInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ChatHubBase, ChatHub>();
            services.AddSignalR();
        }
    }
}
