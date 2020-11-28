using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class CanUpdateMembershipQueryTests
    {
        [Fact]
        public async Task CanUpdateMembershipQueryHandler_ShouldReturnTrue_WhenUserIsPermitted()
        {
            // Arrange
            CanUpdateMembershipQuery request = new CanUpdateMembershipQuery
            {
                GroupMembershipIdToUpdate = 1
            };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.CanUpdateMembership(1, request.GroupMembershipIdToUpdate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            CanUpdateMembershipQuery.Handler handler = new CanUpdateMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canUpdate = await handler.Handle(request);

            // Assert
            Assert.True(canUpdate);
        }

        [Fact]
        public async Task CanUpdateMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotPermitted()
        {
            // Arrange
            CanUpdateMembershipQuery request = new CanUpdateMembershipQuery
            {
                GroupMembershipIdToUpdate = 1
            };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.CanUpdateMembership(1, request.GroupMembershipIdToUpdate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            CanUpdateMembershipQuery.Handler handler = new CanUpdateMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canUpdate = await handler.Handle(request);

            // Assert
            Assert.False(canUpdate);
        }
    }
}
