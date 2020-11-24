using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Moq;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries
{
    public class FriendshipCombinationExistsQueryTests
    {
        [Fact]
        public async Task FriendshipCombinationExistsQueryHandler_ShouldReturnTrue_WhenCombinationExists()
        {
            // Arrange
            FriendshipCombinationExistsQuery request = new FriendshipCombinationExistsQuery
            {
                RequesterId = 1,
                AddresseeId = 2
            };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.CombinationExists(request.RequesterId, request.AddresseeId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            FriendshipCombinationExistsQuery.Handler handler =
                new FriendshipCombinationExistsQuery.Handler(unitOfWorkMock.Object);

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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.CombinationExists(request.RequesterId, request.AddresseeId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            FriendshipCombinationExistsQuery.Handler handler =
                new FriendshipCombinationExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool combinationExists = await handler.Handle(request);

            // Assert
            Assert.False(combinationExists);
        }
    }
}
