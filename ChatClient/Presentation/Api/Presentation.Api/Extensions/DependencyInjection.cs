using Core.Domain.Options;
using Core.Domain.Resources.Errors;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Presentation.Api.Extensions;

public static class DependencyInjection
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add AutoMapper
        services.AddAutoMapper(typeof(Program));

        // Configure options
        services.AddOptions<JwtOptions>().Bind(configuration.GetSection(JwtOptions.ConfigurationKey));
        services.AddOptions<CorsOptions>().Bind(configuration.GetSection(CorsOptions.ConfigurationKey));

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
                    Array.Empty<string>()
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
                
            // Add support for swagger examples
            options.ExampleFilters();
            options.EnableAnnotations();
        });

        // Include validation rules from FluentValidation
        services.AddFluentValidationRulesToSwagger();

        // Add swagger example providers from assemblies
        services.AddSwaggerExamplesFromAssemblyOf<Program>();

        // Add cross-origin-resource-sharing
        services.AddCors();

        // Add support for controllers
        services.AddControllers()
            .AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Program>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
                options.InvalidModelStateResponseFactory = context =>
                {
                    IDictionary<string, IEnumerable<string>> errors = context.ModelState.ToDictionary(
                            state => state.Key,
                            state => state.Value!.Errors.Select(error => error.ErrorMessage)
                        )
                        .Where(pair => pair.Value.Any())
                        .ToDictionary(pair => pair.Key, pair => pair.Value);

                    ValidationErrorResource details = new()
                    {
                        Errors = errors,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                    };

                    return new BadRequestObjectResult(details) { StatusCode = StatusCodes.Status400BadRequest };
                };
            });
    }
}