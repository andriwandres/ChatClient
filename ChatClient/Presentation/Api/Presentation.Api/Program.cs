using Core.Application.Extensions;
using Core.Application.Hubs;
using Core.Domain.Options;
using Core.Domain.Resources.Errors;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Shared.Extensions;
using Infrastructure.Socket.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Api.Extensions;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
 
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add presentation logic related services
builder.Services.AddPresentationServices(builder.Configuration);

// Add persistence logic related infrastructure
builder.Services.AddPersistenceInfrastructureServices(builder.Configuration);

// Add shared infrastructure logic
builder.Services.AddSharedInfrastructureServices(builder.Configuration);

// Add web socket logic
builder.Services.AddSocketInfrastructureServices();

// Add business logic related services
builder.Services.AddApplicationServices();

WebApplication application = builder.Build();

if (application.Environment.IsDevelopment())
{
    application.UseDeveloperExceptionPage();
}

application.UseStatusCodePages(async statusCodeContext =>
{
    statusCodeContext.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

    ErrorResource details = new()
    {
        StatusCode = statusCodeContext.HttpContext.Response.StatusCode,
        Message = ReasonPhrases.GetReasonPhrase(statusCodeContext.HttpContext.Response.StatusCode),
    };

    string json = JsonSerializer.Serialize(details);

    await statusCodeContext.HttpContext.Response.WriteAsync(json);
});

// Perform database migration
using IServiceScope serviceScope = application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
await using ChatContext dbContext = serviceScope.ServiceProvider.GetRequiredService<ChatContext>();
dbContext.Database.Migrate();

// Use swagger UI
application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/master/swagger.json", "Chat Client Web API");
    options.EnableFilter();
    options.DisplayRequestDuration();
    options.ShowExtensions();
    options.ShowCommonExtensions();
    options.EnableValidator();
    options.EnableDeepLinking();
    options.DocExpansion(DocExpansion.List);
});

application.UseHttpsRedirection();

application.UseRouting();

// Configure Cross-Origin-Resource-Sharing
application.UseCors(corsBuilder =>
{
    CorsOptions cors = application.Configuration
        .GetSection(CorsOptions.ConfigurationKey)
        .Get<CorsOptions>();
    
    corsBuilder.WithOrigins(cors!.AllowedOrigins);
    corsBuilder.WithMethods(cors!.AllowedMethods);
    corsBuilder.WithHeaders(cors!.AllowedHeaders);
    corsBuilder.AllowCredentials();
});

application.UseAuthentication();

application.UseAuthorization();

CultureInfo culture = new("en-GB");

application.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = new List<CultureInfo> { culture },
    SupportedUICultures = new List<CultureInfo> { culture },
});

application.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHubBase>("/chat");
    endpoints.MapControllers();
});

application.Run();

public partial class Program {}