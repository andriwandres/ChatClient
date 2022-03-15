using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries;

public class IsOwnRecipientQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public IsOwnRecipientQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userProviderMock = new Mock<IUserProvider>();
        _userProviderMock
            .Setup(m => m.GetCurrentUserId())
            .Returns(1);
    }

    [Fact]
    public async Task IsOwnRecipientQueryHandler_ShouldReturnTrue_WhenRecipientIsOwnUser()
    {
        // Arrange
        IsOwnRecipientQuery request = new() { RecipientId = 1 };

        Recipient databaseRecipient = new() { RecipientId = 1, UserId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Recipients.GetByIdAsync(request.RecipientId))
            .ReturnsAsync(databaseRecipient);

        IsOwnRecipientQuery.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool isOwnRecipient = await handler.Handle(request);

        // Assert
        Assert.True(isOwnRecipient);
    }

    [Fact]
    public async Task IsOwnRecipientQueryHandler_ShouldReturnFalse_WhenRecipientIsForeignUser()
    {
        // Arrange
        IsOwnRecipientQuery request = new() { RecipientId = 1 };
        Recipient databaseRecipient = new() { RecipientId = 1, UserId = 2 };

        _unitOfWorkMock
            .Setup(m => m.Recipients.GetByIdAsync(request.RecipientId))
            .ReturnsAsync(databaseRecipient);

        IsOwnRecipientQuery.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool isOwnRecipient = await handler.Handle(request);

        // Assert
        Assert.False(isOwnRecipient);
    }

    [Fact]
    public async Task IsOwnRecipientQueryHandler_ShouldReturnFalse_WhenRecipientIsForeignGroup()
    {
        // Arrange
        IsOwnRecipientQuery request = new() { RecipientId = 1 };

        Recipient databaseRecipient = new() { RecipientId = 1, GroupMembershipId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Recipients.GetByIdAsync(request.RecipientId))
            .ReturnsAsync(databaseRecipient);

        IsOwnRecipientQuery.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool isOwnRecipient = await handler.Handle(request);

        // Assert
        Assert.False(isOwnRecipient);
    }
}