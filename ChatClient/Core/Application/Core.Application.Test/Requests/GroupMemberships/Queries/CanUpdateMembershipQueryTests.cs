using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Services;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries;

public class CanUpdateMembershipQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public CanUpdateMembershipQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userProviderMock = new Mock<IUserProvider>();
        _userProviderMock
            .Setup(m => m.GetCurrentUserId())
            .Returns(1);
    }

    [Fact]
    public async Task CanUpdateMembershipQueryHandler_ShouldReturnTrue_WhenUserIsPermitted()
    {
        // Arrange
        CanUpdateMembershipQuery request = new CanUpdateMembershipQuery
        {
            GroupMembershipIdToUpdate = 1
        };

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.CanUpdateMembership(1, request.GroupMembershipIdToUpdate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        CanUpdateMembershipQuery.Handler handler = new CanUpdateMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool canUpdate = await handler.Handle(request);

        // Assert
        Assert.True(canUpdate);
    }

    [Fact]
    public async Task CanUpdateMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotPermitted()
    {
        // Arrange
        CanUpdateMembershipQuery request = new CanUpdateMembershipQuery
        {
            GroupMembershipIdToUpdate = 1
        };

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.CanUpdateMembership(1, request.GroupMembershipIdToUpdate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        CanUpdateMembershipQuery.Handler handler = new CanUpdateMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        bool canUpdate = await handler.Handle(request);

        // Assert
        Assert.False(canUpdate);
    }
}