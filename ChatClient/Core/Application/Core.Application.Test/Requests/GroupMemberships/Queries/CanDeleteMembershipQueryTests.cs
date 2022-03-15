using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Services;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries;

public class CanDeleteMembershipQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public CanDeleteMembershipQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userProviderMock = new Mock<IUserProvider>();
        _userProviderMock
            .Setup(m => m.GetCurrentUserId())
            .Returns(1);
    }

    [Fact]
    public async Task CanDeleteMembershipQueryHandler_ShouldReturnTrue_WhenUserIsPermitted()
    {
        // Arrange
        CanDeleteMembershipQuery request = new CanDeleteMembershipQuery
        {
            GroupMembershipIdToDelete = 1
        };

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.CanDeleteMembership(1, request.GroupMembershipIdToDelete, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        CanDeleteMembershipQuery.Handler handler = new CanDeleteMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool canDelete = await handler.Handle(request);

        // Assert
        Assert.True(canDelete);
    }

    [Fact]
    public async Task CanDeleteMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotPermitted()
    {
        // Arrange
        CanDeleteMembershipQuery request = new CanDeleteMembershipQuery
        {
            GroupMembershipIdToDelete = 1
        };

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.CanDeleteMembership(1, request.GroupMembershipIdToDelete, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        CanDeleteMembershipQuery.Handler handler = new CanDeleteMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool canDelete = await handler.Handle(request);

        // Assert
        Assert.False(canDelete);
    }
}