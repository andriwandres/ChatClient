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
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public EmailExistsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task EmailExistsQueryHandler_ShouldReturnTrue_WhenEmailExists()
        {
            // Arrange
            EmailExistsQuery query = new EmailExistsQuery { Email = "test@test.test"};

            _unitOfWorkMock
                .Setup(m => m.Users.EmailExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            EmailExistsQuery.Handler handler = new EmailExistsQuery.Handler(_unitOfWorkMock.Object);

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

            _unitOfWorkMock
                .Setup(m => m.Users.EmailExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            EmailExistsQuery.Handler handler = new EmailExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
        }
    }
}
