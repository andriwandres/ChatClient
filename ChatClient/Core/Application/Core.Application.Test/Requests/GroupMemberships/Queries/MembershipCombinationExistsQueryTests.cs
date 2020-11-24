using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class MembershipCombinationExistsQueryTests
    {
        [Fact]
        public async Task MembershipCombinationExistsQueryHandler_ShouldReturnTrue_WhenCombinationExists()
        {
            // Arrange
            MembershipCombinationExistsQuery request = new MembershipCombinationExistsQuery
            {
                UserId = 1,
                GroupId = 1,
            };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.CombinationExists(request.GroupId, request.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            MembershipCombinationExistsQuery.Handler handler = new MembershipCombinationExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task MembershipCombinationExistsQueryHandler_ShouldReturnFalse_WhenCombinationDoesNotExist()
        {
            // Arrange
            MembershipCombinationExistsQuery request = new MembershipCombinationExistsQuery
            {
                UserId = 124,
                GroupId = 421,
            };

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.CombinationExists(request.GroupId, request.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MembershipCombinationExistsQuery.Handler handler = new MembershipCombinationExistsQuery.Handler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
