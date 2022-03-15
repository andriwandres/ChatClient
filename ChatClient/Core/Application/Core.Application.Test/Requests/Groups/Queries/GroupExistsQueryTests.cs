using Core.Application.Database;
using Core.Application.Requests.Groups.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Queries;

public class GroupExistsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public GroupExistsQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task GroupExistsQueryHandler_ShouldReturnTrue_WhenGroupExists()
    {
        // Arrange
        GroupExistsQuery request = new GroupExistsQuery { GroupId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Groups.Exists(request.GroupId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        GroupExistsQuery.Handler handler = new GroupExistsQuery.Handler(_unitOfWorkMock.Object);

        // Act
        bool exists = await handler.Handle(request);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task GroupExistsQueryHandler_ShouldReturnFalse_WhenGroupDoesNotExist()
    {
        // Arrange
        GroupExistsQuery request = new GroupExistsQuery { GroupId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Groups.Exists(request.GroupId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        GroupExistsQuery.Handler handler = new GroupExistsQuery.Handler(_unitOfWorkMock.Object);

        // Act
        bool exists = await handler.Handle(request);

        // Assert
        Assert.False(exists);
    }
}