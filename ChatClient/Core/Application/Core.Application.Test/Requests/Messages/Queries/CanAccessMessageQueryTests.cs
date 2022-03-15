using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Services;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries;

public class CanAccessMessageQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public CanAccessMessageQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userProviderMock = new Mock<IUserProvider>();

        _userProviderMock
            .Setup(m => m.GetCurrentUserId())
            .Returns(1);
    }

    [Fact]
    public async Task CanAccessMessageQueryHandler_ShouldReturnTrue_WhenUserCanAccessMessage()
    {
        // Arrange
        CanAccessMessageQuery request = new CanAccessMessageQuery {MessageId = 1};

        _unitOfWorkMock
            .Setup(m => m.Messages.CanAccess(request.MessageId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        CanAccessMessageQuery.Handler handler = new CanAccessMessageQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool canAccess = await handler.Handle(request);

        // Assert
        Assert.True(canAccess);
    }

    [Fact]
    public async Task CanAccessMessageQueryHandler_ShouldReturnFalse_WhenUserIsNotAllowedToAccessMessage()
    {
        // Arrange
        CanAccessMessageQuery request = new CanAccessMessageQuery { MessageId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Messages.CanAccess(request.MessageId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        CanAccessMessageQuery.Handler handler = new CanAccessMessageQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool canAccess = await handler.Handle(request);

        // Assert
        Assert.False(canAccess);
    }
}