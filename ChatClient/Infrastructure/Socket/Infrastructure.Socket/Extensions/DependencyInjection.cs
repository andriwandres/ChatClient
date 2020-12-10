using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Socket.Extensions
{
    public static class DependencyInjection
    {
        public static void AddSocketInfrastructureServices(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }
    }
}
