using Core.Application.Database;
using Core.Application.Requests.Groups.Commands;
using Core.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Commands;

public class DeleteGroupCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteGroupCommandTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task DeleteGroupCommandHandler_ShouldSetDeleteFlagOnGroup()
    {
        // Arrange
        DeleteGroupCommand request = new() { GroupId = 1 };

        Group expectedGroup = new() { GroupId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Groups.GetByIdAsync(request.GroupId))
            .ReturnsAsync(expectedGroup);

        Group passedGroup = null;

        _unitOfWorkMock
            .Setup(m => m.Groups.Update(It.IsAny<Group>()))
            .Callback<Group>(g => passedGroup = g);

        DeleteGroupCommand.Handler handler = new(_unitOfWorkMock.Object);

        // Act
        await handler.Handle(request);

        // Assert
        Assert.NotNull(passedGroup);
        Assert.Equal(request.GroupId, passedGroup.GroupId);
        Assert.True(passedGroup.IsDeleted);

        _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}