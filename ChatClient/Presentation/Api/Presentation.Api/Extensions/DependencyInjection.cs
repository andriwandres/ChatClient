using AutoMapper;
using Core.Domain.Options;
using Core.Domain.Resources.Errors;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Presentation.Api.Extensions
{
    public static class DependencyInjection
    {
        public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Configure options
            services.Configure<JwtOptions>(configuration.GetSection("JsonWebToken"));
            services.Configure<CorsOptions>(configuration.GetSection("CrossOriginResourceSharing"));

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

                //options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.EnableAnnotations();
            });

            // Add swagger example providers from assemblies
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            // Add cross-origin-resource-sharing
            services.AddCors();

            // Add support for controllers
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressMapClientErrors = true;
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        IDictionary<string, IEnumerable<string>> errors = context.ModelState.ToDictionary(
                            state => state.Key,
                            state => state.Value.Errors.Select(error => error.ErrorMessage)
                        );

                        ValidationErrorResource details = new ValidationErrorResource
                        {
                            Errors = errors,
                            StatusCode = 400,
                            Message = "One or multiple validation errors occurred",
                        };

                        return new ObjectResult(details) {StatusCode = 400};
                    };
                });
        }
    }
}
