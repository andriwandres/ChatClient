using Core.Application.Database;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Extensions;

public static class DependencyInjection
{
    public static void AddPersistenceInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string migrationAssemblyName = typeof(ChatContext).Assembly.FullName;
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add database connection
        services.AddDbContext<ChatContext>(builder =>
        {
            builder.UseSqlServer(connectionString, options =>
            {
                options.MigrationsAssembly(migrationAssemblyName);
            });
        });

        // Use abstraction of db context in service collection
        services.AddTransient<IChatContext, ChatContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}