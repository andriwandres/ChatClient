using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Presentation.Api.Test
{
    public class StartupTests
    {
        private readonly IDictionary<string, string> _configurationDictionary = new Dictionary<string, string>
        {
            { "ConnectionStrings:DefaultConnection", "TestConnectionString" },
            { "JsonWebToken:Secret", "some_secret_string" },
            { "CrossOriginResourceSharing:AllowedOrigins:0", "http://localhost" },
            { "CrossOriginResourceSharing:AllowedMethods:0", "GET" },
            { "CrossOriginResourceSharing:AllowedHeaders:0", "*" },
        };

        [Fact]
        public void ConfigureServices_ShouldCorrectlyRegisterDependencies()
        {
            // Arrange
            IConfiguration configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(_configurationDictionary)
                .Build();

            IServiceCollection serviceCollectionMock = new ServiceCollection();

            Startup startup = new Startup(configurationMock);

            // Act + Assert
            startup.ConfigureServices(serviceCollectionMock);
        }
    }
}
