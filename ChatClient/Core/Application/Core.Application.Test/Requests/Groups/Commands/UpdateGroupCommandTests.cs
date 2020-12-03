using Core.Application.Database;
using Core.Application.Requests.Groups.Commands;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Commands
{
    public class UpdateGroupCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdateGroupCommandTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task UpdateGroupCommandHandler_ShouldUpdateGroup()
        {
            // Arrange
            UpdateGroupCommand request = new UpdateGroupCommand
            {
                GroupId = 1,
                Name = "Some updated name",
                Description = "Some updated description"
            };

            IEnumerable<Group> expectedGroup = new[]
            {
                new Group { GroupId = 1 }
            };

            IQueryable<Group> queryableMock = expectedGroup
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWork
                .Setup(m => m.Groups.GetById(request.GroupId))
                .Returns(queryableMock);

            UpdateGroupCommand.Handler handler = new UpdateGroupCommand.Handler(_unitOfWork.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWork.Verify(m => m.Groups.Update(It.IsAny<Group>()), Times.Once);
            _unitOfWork.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
