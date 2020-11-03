using Core.Application.Services;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared.Extensions
{
    public static class DependencyInjection
    {
        public static void AddSharedInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IDateProvider, DateProvider>();
        }
    }
}
