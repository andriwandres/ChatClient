using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries;

public class FriendshipCombinationExistsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public FriendshipCombinationExistsQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task FriendshipCombinationExistsQueryHandler_ShouldReturnTrue_WhenCombinationExists()
    {
        // Arrange
        FriendshipCombinationExistsQuery request = new FriendshipCombinationExistsQuery
        {
            RequesterId = 1,
            AddresseeId = 2
        };

        _unitOfWorkMock
            .Setup(m => m.Friendships.CombinationExists(request.RequesterId, request.AddresseeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        FriendshipCombinationExistsQuery.Handler handler = new FriendshipCombinationExistsQuery.Handler(_unitOfWorkMock.Object);

        // Act
        bool combinationExists = await handler.Handle(request);

        // Assert
        Assert.True(combinationExists);
    }

    [Fact]
    public async Task FriendshipCombinationExistsQueryHandler_ShouldReturnFalse_WhenCombinationDoesNotExist()
    {
        // Arrange
        FriendshipCombinationExistsQuery request = new FriendshipCombinationExistsQuery
        {
            RequesterId = 8761,
            AddresseeId = 242
        };

        _unitOfWorkMock
            .Setup(m => m.Friendships.CombinationExists(request.RequesterId, request.AddresseeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        FriendshipCombinationExistsQuery.Handler handler = new FriendshipCombinationExistsQuery.Handler(_unitOfWorkMock.Object);

        // Act
        bool combinationExists = await handler.Handle(request);

        // Assert
        Assert.False(combinationExists);
    }
}