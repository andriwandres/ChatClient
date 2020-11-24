using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class MembershipExistsQueryTests
    {
        [Fact]
        public async Task MembershipExistsQuery_ShouldReturnTrue_WhenMembeshipExists()
        {
            // Arrange
            MembershipExistsQuery request = new MembershipExistsQuery {GroupMembershipId = 1};

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.Exists(request.GroupMembershipId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            MembershipExistsQuery.Handler handler = new MembershipExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task MembershipExistsQuery_ShouldReturnFalse_WhenMembeshipDoesNotExist()
        {
            // Arrange
            MembershipExistsQuery request = new MembershipExistsQuery { GroupMembershipId = 411 };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.Exists(request.GroupMembershipId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MembershipExistsQuery.Handler handler = new MembershipExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
