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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(queryableMock);

            GroupMembership passedMembership = null;

            unitOfWorkMock
                .Setup(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()))
                .Callback<GroupMembership>(gm => passedMembership = gm);

            unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UpdateMembershipCommand.Handler handler = new UpdateMembershipCommand.Handler(unitOfWorkMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            Assert.NotNull(passedMembership);
            Assert.Equal(request.IsAdmin, passedMembership.IsAdmin);

            unitOfWorkMock.Verify(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()), Times.AtLeastOnce);
            unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
