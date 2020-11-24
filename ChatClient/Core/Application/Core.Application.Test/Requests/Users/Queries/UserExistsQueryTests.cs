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
        [Fact]
        public async Task UserExistsQueryHandler_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            UserExistsQuery request = new UserExistsQuery { UserId = 1 };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.Exists(request.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserExistsQuery.Handler handler = new UserExistsQuery.Handler(unitOfWorkMock.Object);

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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.Exists(request.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserExistsQuery.Handler handler = new UserExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
