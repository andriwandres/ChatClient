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
        [Fact]
        public async Task UserNameExistsQueryHandler_ShouldReturnTrue_WhenUserNameExists()
        {
            // Arrange
            UserNameExistsQuery query = new UserNameExistsQuery { UserName = "username1" };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.UserNameExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserNameExistsQuery.UserNameExistsQueryHandler handler = new UserNameExistsQuery.UserNameExistsQueryHandler(unitOfWorkMock.Object);

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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.UserNameExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserNameExistsQuery.UserNameExistsQueryHandler handler = new UserNameExistsQuery.UserNameExistsQueryHandler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
        }
    }
}
