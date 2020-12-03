using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class UserNameExistsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UserNameExistsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task UserNameExistsQueryHandler_ShouldReturnTrue_WhenUserNameExists()
        {
            // Arrange
            UserNameExistsQuery query = new UserNameExistsQuery { UserName = "username1" };

            _unitOfWorkMock
                .Setup(m => m.Users.UserNameExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserNameExistsQuery.Handler handler = new UserNameExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(query);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserNameExistsQueryHandler_ShouldReturnFalse_WhenUserNameDoesNotExists()
        {
            // Arrange
            UserNameExistsQuery query = new UserNameExistsQuery { UserName = "invalidusername1" };

            _unitOfWorkMock
                .Setup(m => m.Users.UserNameExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserNameExistsQuery.Handler handler = new UserNameExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
        }
    }
}
