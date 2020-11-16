using Core.Application.Extensions;
using Core.Domain.Options;
using Core.Domain.Resources.Errors;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Presentation.Api.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Presentation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add presentation logic related services
            services.AddPresentationServices(Configuration);

            // Add persistence logic related infrastructure
            services.AddPersistenceInfrastructureServices(Configuration);

            // Add shared infrastructure logic
            services.AddSharedInfrastructureServices(Configuration);

            // Add business logic related services
            services.AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<CorsOptions> cors)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

                ErrorResource details = new ErrorResource
                {
                    StatusCode = context.HttpContext.Response.StatusCode,
                    Message = ReasonPhrases.GetReasonPhrase(context.HttpContext.Response.StatusCode),
                };

                string json = JsonSerializer.Serialize(details);

                await context.HttpContext.Response.WriteAsync(json);
            });

            // Use swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/master/swagger.json", "Chat Client Web API");
                options.DocExpansion(DocExpansion.List);
                options.EnableFilter();
                options.DisplayRequestDuration();
                options.ShowExtensions();
                options.ShowCommonExtensions();
                options.EnableValidator();
                options.EnableDeepLinking();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins(cors.Value.AllowedOrigins);
                builder.WithMethods(cors.Value.AllowedMethods);
                builder.WithHeaders(cors.Value.AllowedHeaders);
            });

            app.UseAuthentication();

            app.UseAuthorization();

            CultureInfo culture = new CultureInfo("en-GB");

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture),
                SupportedCultures = new List<CultureInfo> { culture },
                SupportedUICultures = new List<CultureInfo> { culture },
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
