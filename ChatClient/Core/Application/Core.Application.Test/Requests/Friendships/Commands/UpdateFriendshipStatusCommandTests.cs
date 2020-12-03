using Core.Application.Database;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Commands
{
    public class UpdateFriendshipStatusCommandTests
    {
        private readonly Mock<IDateProvider> _dateProviderMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateFriendshipStatusCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _dateProviderMock = new Mock<IDateProvider>();
            _dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1));
        }

        [Fact]
        public async Task UpdateFriendshipStatusCommandHandler_ShouldAddNewFriendshipChange()
        {
            // Arrange
            UpdateFriendshipStatusCommand request = new UpdateFriendshipStatusCommand
            {
                FriendshipId = 1,
                FriendshipStatusId = FriendshipStatusId.Accepted
            };

            _unitOfWorkMock
                .Setup(m => m.FriendshipChanges.Add(It.IsAny<FriendshipChange>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UpdateFriendshipStatusCommand.Handler handler = new UpdateFriendshipStatusCommand.Handler(_unitOfWorkMock.Object, _dateProviderMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.FriendshipChanges.Add(It.IsAny<FriendshipChange>(), It.IsAny<CancellationToken>()));
            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()));
        }
    }
}
