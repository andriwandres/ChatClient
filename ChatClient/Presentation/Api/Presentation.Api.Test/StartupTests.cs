using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Presentation.Api.Test
{
    public class StartupTests
    {
        [Fact]
        public void ConfigureServices_ShouldCorrectlyRegisterDependencies()
        {
            // Arrange
            Dictionary<string, string> configurationDictionary = new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", "TestConnectionString" },
                { "JsonWebToken:Secret", "some_secret_string" },
                { "CrossOriginResourceSharing:AllowedOrigins:0", "https://google.com" },
                { "CrossOriginResourceSharing:AllowedMethods:0", "GET" },
                { "CrossOriginResourceSharing:AllowedHeaders:0", "*" },
            };

            IConfiguration configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(configurationDictionary)
                .Build();

            IServiceCollection serviceCollectionMock = new ServiceCollection();

            Startup startup = new Startup(configurationMock);

            // Act
            startup.ConfigureServices(serviceCollectionMock);

            // Assert
        }
    }
}
