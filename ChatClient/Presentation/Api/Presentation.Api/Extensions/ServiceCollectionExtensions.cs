using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Presentation.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPresentationServices(this IServiceCollection services)
        {
            // Add swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("master", new OpenApiInfo
                {
                    Title = "Chat Client API",
                    Description = "REST API for instant-messenger web client",
                });

                // Define jwt bearer authentication for securing the api in swagger
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Define jwt bearer as the security requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
                        },
                        new string[] {}
                    }
                });

                // Add xml documentation across all assemblies
                Assembly currentAssembly = Assembly.GetExecutingAssembly();

                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                string[] xmlPaths = assemblies
                    .Select(assembly => Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? string.Empty,
                        $"{assembly.GetName().Name}.xml"))
                    .Where(File.Exists)
                    .ToArray();

                foreach (string path in xmlPaths)
                {
                    options.IncludeXmlComments(path);
                }

                // Include validation rules from FluentValidation
                options.AddFluentValidationRules();

                // Add support for swagger examples
                options.ExampleFilters();
            });

            // Add swagger example providers from assemblies
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            // Add support for controllers
            services.AddControllers()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
        }
    }
}
