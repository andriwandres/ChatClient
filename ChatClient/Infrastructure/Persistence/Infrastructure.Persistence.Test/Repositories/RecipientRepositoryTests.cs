using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class RecipientRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddRecipientsToTheDbContext()
        {
            // Arrange
            Recipient recipient = new Recipient();

            Mock<DbSet<Recipient>> membershipDbSetMock = Enumerable
                .Empty<Recipient>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Recipients)
                .Returns(membershipDbSetMock.Object);

            RecipientRepository repository = new RecipientRepository(contextMock.Object);

            // Act
            await repository.Add(recipient);

            // Assert
            contextMock.Verify(m => m.Recipients.AddAsync(recipient, It.IsAny<CancellationToken>()));
        }
    }
}
