using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
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
            UpdateMembershipCommand request = new UpdateMembershipCommand
            {
                GroupMembershipId = 1,
                IsAdmin = true
            };

            IEnumerable<GroupMembership> expectedMemberships = new[]
            {
                new GroupMembership {GroupMembershipId = 1, UserId = 1, IsAdmin = false}
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(queryableMock);

            GroupMembership passedMembership = null;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()))
                .Callback<GroupMembership>(gm => passedMembership = gm);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UpdateMembershipCommand.Handler handler = new UpdateMembershipCommand.Handler(_unitOfWorkMock.Object);

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
