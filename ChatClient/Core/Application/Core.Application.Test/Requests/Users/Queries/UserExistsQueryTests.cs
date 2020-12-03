using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class UserExistsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UserExistsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task UserExistsQueryHandler_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            UserExistsQuery request = new UserExistsQuery { UserId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Users.Exists(request.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserExistsQuery.Handler handler = new UserExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserExistsQueryHandler_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            UserExistsQuery request = new UserExistsQuery { UserId = 124311 };

            _unitOfWorkMock
                .Setup(m => m.Users.Exists(request.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserExistsQuery.Handler handler = new UserExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
