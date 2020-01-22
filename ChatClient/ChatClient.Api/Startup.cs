using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Core;
using ChatClient.Core.Options;
using ChatClient.Data;
using ChatClient.Data.Database;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChatClient.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            // Add Database Context
            services.AddDbContext<ChatContext>(builder =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");

                builder.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly("ChatClient.Data");
                });
            });

            var jwtSection = Configuration.GetSection("JsonWebToken");
            var jwtSettings = jwtSection.Get<JwtOptions>();
            var jwtSecret = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var corsSection = Configuration.GetSection("CrossOriginResourceSharing");

            services.Configure<CorsOptions>(corsSection);
            services.Configure<JwtOptions>(jwtSection);

            // Add HTTP Context Accessor for accessing logged in user
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Cross-Origin-Resource-Sharing
            services.AddCors();

            // Add Support for Controllers
            services.AddControllers()

                // Add FluentValidation
                .AddFluentValidation(configuration =>
                {
                    configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<CorsOptions> corsOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use JWT Authentication
            app.UseAuthentication();

            // Use Routing
            app.UseRouting();

            CorsOptions cors = corsOptions.Value;

            // Use Cross-Origin-Resource-Sharing
            app.UseCors(builder =>
            {
                builder.WithOrigins(cors.AllowedOrigins);
                builder.WithMethods(cors.AllowedMethods);
                builder.WithHeaders(cors.AllowedHeaders);
            });

            // Use Authorization
            app.UseAuthorization();

            // Map Controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
