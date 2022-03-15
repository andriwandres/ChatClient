using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries;

public class UserNameOrEmailExistsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public UserNameOrEmailExistsQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task GetUserNameOrEmailExistsQueryHandler_ShouldReturnTrue_WhenCredentialsExist()
    {
        UserNameOrEmailExistsQuery request = new UserNameOrEmailExistsQuery
        {
            Email = "test@test.test",
            UserName = "testtesttest"
        };

        // Arrange
        _unitOfWorkMock
            .Setup(m => m.Users.UserNameOrEmailExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        UserNameOrEmailExistsQuery.Handler handler =
            new UserNameOrEmailExistsQuery.Handler(_unitOfWorkMock.Object);

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
        _unitOfWorkMock
            .Setup(m => m.Users.UserNameOrEmailExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        UserNameOrEmailExistsQuery.Handler handler =
            new UserNameOrEmailExistsQuery.Handler(_unitOfWorkMock.Object);

        // Act
        bool exists = await handler.Handle(request);

        // Assert
        Assert.False(exists);
    }
}