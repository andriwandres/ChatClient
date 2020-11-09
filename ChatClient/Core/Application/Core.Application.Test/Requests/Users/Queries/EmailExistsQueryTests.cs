using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class EmailExistsQueryTests
    {
        [Fact]
        public async Task EmailExistsQueryHandler_ShouldReturnTrue_WhenEmailExists()
        {
            // Arrange
            EmailExistsQuery query = new EmailExistsQuery { Email = "test@test.test"};

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.EmailExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            EmailExistsQuery.EmailExistsQueryHandler handler = new EmailExistsQuery.EmailExistsQueryHandler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(query);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task EmailExistsQueryHandler_ShouldReturnFalse_WhenEmailDoesNotExists()
        {
            // Arrange
            EmailExistsQuery query = new EmailExistsQuery { Email = "invalid@email.address" };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.EmailExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            EmailExistsQuery.EmailExistsQueryHandler handler = new EmailExistsQuery.EmailExistsQueryHandler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
        }
    }
}
