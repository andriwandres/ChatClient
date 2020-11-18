using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries
{
    public class FriendshipExistsQueryTests
    {
        [Fact]
        public async Task FriendshipExistsQueryHandler_ShouldReturnTrue_WhenFriendshipExists()
        {
            // Arrange
            FriendshipExistsQuery request = new FriendshipExistsQuery { FriendshipId = 1 };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.Exists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            FriendshipExistsQuery.Handler handler = new FriendshipExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task FriendshipExistsQueryHandler_ShouldReturnFalse_WhenFriendshipDoesNotExist()
        {
            // Arrange
            FriendshipExistsQuery request = new FriendshipExistsQuery { FriendshipId = 6531 };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.Exists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            FriendshipExistsQuery.Handler handler = new FriendshipExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
