using Core.Application.Database;
using Core.Application.Requests.Groups.Commands;
using Core.Domain.Entities;
using Moq;
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
            UpdateGroupCommand request = new()
            {
                GroupId = 1,
                Name = "Some updated name",
                Description = "Some updated description"
            };

            Group expectedGroup = new() { GroupId = 1 };

            _unitOfWork
                .Setup(m => m.Groups.GetByIdAsync(request.GroupId))
                .ReturnsAsync(expectedGroup);

            UpdateGroupCommand.Handler handler = new(_unitOfWork.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWork.Verify(m => m.Groups.Update(It.IsAny<Group>()), Times.Once);
            _unitOfWork.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
