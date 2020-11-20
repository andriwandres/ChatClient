using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Database
{
    public class DbContextTests
    {
        [Fact]
        public async Task OnModelCreating_ShouldConfigureEntities()
        {
            // Arrange
            DbContextOptions<ChatContext> options = new DbContextOptionsBuilder<ChatContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            await using ChatContext context = new ChatContext(options);

            // Act + Assert
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
