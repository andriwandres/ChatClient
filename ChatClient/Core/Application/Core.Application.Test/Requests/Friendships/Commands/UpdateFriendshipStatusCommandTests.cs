using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Commands
{
    public class UpdateFriendshipStatusCommandTests
    {
        [Fact]
        public async Task UpdateFriendshipStatusCommandHandler_ShouldAddNewFriendshipChange()
        {
            // Arrange
            UpdateFriendshipStatusCommand request = new UpdateFriendshipStatusCommand
            {
                FriendshipId = 1,
                FriendshipStatusId = FriendshipStatusId.Accepted
            };

            Mock<IDateProvider> dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1, 0, 0, 0));

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.FriendshipChanges.Add(It.IsAny<FriendshipChange>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UpdateFriendshipStatusCommand.Handler handler = new UpdateFriendshipStatusCommand.Handler(unitOfWorkMock.Object, dateProviderMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            unitOfWorkMock.Verify(m => m.FriendshipChanges.Add(It.IsAny<FriendshipChange>(), It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()));
        }
    }
}
