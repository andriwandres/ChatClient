using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Core;
using ChatClient.Core.Options;
using ChatClient.Core.Services;
using ChatClient.Data;
using ChatClient.Data.Database;
using ChatClient.Services;
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
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerUI;

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

            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            var jwtSection = Configuration.GetSection("JsonWebToken");
            var jwtSettings = jwtSection.Get<JwtOptions>();
            var jwtSecret = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var corsSection = Configuration.GetSection("CrossOriginResourceSharing");

            // Add Options
            services.Configure<CorsOptions>(corsSection);
            services.Configure<JwtOptions>(jwtSection);

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
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                // Attach SignalR Access Token
                builder.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        PathString path = context.HttpContext.Request.Path;
                        string token = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/hubs/chat"))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSignalR();

            // Add Swagger
            services.AddSwaggerGen(options =>
            {
                // Swagger Document
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chat Client Web API",
                    Description = "REST API for Instant-Messenger Web Client",
                });

                // Add Bearer Security Definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Add Bearer Security Requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });

                // Add XML Documentation
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // Add HTTP Context Accessor for accessing logged in user
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add services
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IAuthService, AuthService>();

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

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat Client Web API");
                options.DocExpansion(DocExpansion.List);
            });

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
                // endpoints.MapHubs<ChatHub>("/chat");
            });
        }
    }
}
