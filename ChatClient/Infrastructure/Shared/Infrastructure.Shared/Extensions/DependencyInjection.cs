using Core.Application.Services;
using Core.Domain.Options;
using Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Extensions
{
    public static class DependencyInjection
    {
        public static void AddSharedInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add infrastructure services
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IDateProvider, DateProvider>();
            services.AddTransient<IUserProvider, UserProvider>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Get jwt secret
            IConfigurationSection jwtSection = configuration.GetSection("JsonWebToken");
            string secretString = jwtSection.Get<JwtOptions>().Secret;
            byte[] secretBytes = Encoding.ASCII.GetBytes(secretString);

            // Add jwt bearer authentication
            services.AddAuthentication(builder =>
            {
                builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(builder =>
            {
                builder.RequireHttpsMetadata = false;
                builder.SaveToken = true;
                builder.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // Attach SignalR access token
                builder.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        PathString path = context.HttpContext.Request.Path;
                        string token = context.Request.Query["access_token"];

                        if (!string.IsNullOrWhiteSpace(token) && path.StartsWithSegments("/chat"))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
