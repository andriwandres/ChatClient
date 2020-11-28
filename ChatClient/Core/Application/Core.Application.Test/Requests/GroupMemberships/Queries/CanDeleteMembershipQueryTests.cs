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
    public class CanDeleteMembershipQueryTests
    {
        [Fact]
        public async Task CanDeleteMembershipQueryHandler_ShouldReturnTrue_WhenUserIsPermitted()
        {
            // Arrange
            CanDeleteMembershipQuery request = new CanDeleteMembershipQuery
            {
                GroupMembershipIdToDelete = 1
            };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.CanDeleteMembership(1, request.GroupMembershipIdToDelete, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            CanDeleteMembershipQuery.Handler handler = new CanDeleteMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canDelete = await handler.Handle(request);

            // Assert
            Assert.True(canDelete);
        }

        [Fact]
        public async Task CanDeleteMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotPermitted()
        {
            // Arrange
            CanDeleteMembershipQuery request = new CanDeleteMembershipQuery
            {
                GroupMembershipIdToDelete = 1
            };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.CanDeleteMembership(1, request.GroupMembershipIdToDelete, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            CanDeleteMembershipQuery.Handler handler = new CanDeleteMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canDelete = await handler.Handle(request);

            // Assert
            Assert.False(canDelete);
        }
    }
}
