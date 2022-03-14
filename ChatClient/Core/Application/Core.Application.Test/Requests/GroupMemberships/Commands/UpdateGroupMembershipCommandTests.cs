using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Commands
{
    public class UpdateGroupMembershipCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateGroupMembershipCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task UpdateMembershipCommandHandler_ShouldUpdateMembership()
        {
            // Arrange
            UpdateMembershipCommand request = new()
            {
                GroupMembershipId = 1,
                IsAdmin = true
            };

            GroupMembership expectedMembership = new() { GroupMembershipId = 1, UserId = 1, IsAdmin = false };

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByIdAsync(request.GroupMembershipId))
                .ReturnsAsync(expectedMembership);

            GroupMembership passedMembership = null;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()))
                .Callback<GroupMembership>(gm => passedMembership = gm);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UpdateMembershipCommand.Handler handler = new(_unitOfWorkMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            Assert.NotNull(passedMembership);
            Assert.Equal(request.IsAdmin, passedMembership.IsAdmin);

            _unitOfWorkMock.Verify(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
