using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class GetUserNameOrEmailExistsQueryTests
    {
        [Fact]
        public async Task GetUserNameOrEmailExistsQueryHandler_ShouldReturnTrue_WhenCredentialsExist()
        {
            UserNameOrEmailExistsQuery request = new UserNameOrEmailExistsQuery
            {
                Email = "test@test.test",
                UserName = "testtesttest"
            };

            // Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.UserNameOrEmailExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserNameOrEmailExistsQuery.UserNameOrEmailQueryHandler handler =
                new UserNameOrEmailExistsQuery.UserNameOrEmailQueryHandler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task GetUserNameOrEmailExistsQueryHandler_ShouldReturnFalse_WhenCredentialsDontNotExist()
        {
            UserNameOrEmailExistsQuery request = new UserNameOrEmailExistsQuery
            {
                Email = "invalid@email.address",
                UserName = "testtesttest"
            };

            // Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.UserNameOrEmailExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserNameOrEmailExistsQuery.UserNameOrEmailQueryHandler handler =
                new UserNameOrEmailExistsQuery.UserNameOrEmailQueryHandler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
