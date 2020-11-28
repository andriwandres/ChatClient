using System.Collections.Generic;
using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class CanCreateMembershipQueryTests
    {
        [Fact]
        public async Task CanCreateMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotPartOfGroup()
        {
            // Arrange
            CanCreateMembershipQuery request = new CanCreateMembershipQuery { GroupId = 1 };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            IQueryable<GroupMembership> expectedMemberships = Enumerable
                .Empty<GroupMembership>()
                .AsQueryable()
                .BuildMock()
                .Object;

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByCombination(request.GroupId, 1))
                .Returns(expectedMemberships);
            
            CanCreateMembershipQuery.Handler handler = new CanCreateMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canCreate = await handler.Handle(request);

            // Assert
            Assert.False(canCreate);
        }

        [Fact]
        public async Task CanCreateMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotAnAdministrator()
        {
            // Arrange
            CanCreateMembershipQuery request = new CanCreateMembershipQuery { GroupId = 1 };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            IEnumerable<GroupMembership> expectedMemberships = new[]
            {
                new GroupMembership {GroupMembershipId = 1, UserId = 1, IsAdmin = false}
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByCombination(request.GroupId, 1))
                .Returns(queryableMock);

            CanCreateMembershipQuery.Handler handler = new CanCreateMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canCreate = await handler.Handle(request);

            // Assert
            Assert.False(canCreate);
        }

        [Fact]
        public async Task CanCreateMembershipQueryHandler_ShouldReturnTrue_WhenUserIsAnAdministrator()
        {
            // Arrange
            CanCreateMembershipQuery request = new CanCreateMembershipQuery { GroupId = 1 };

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            IEnumerable<GroupMembership> expectedMemberships = new[]
            {
                new GroupMembership {GroupMembershipId = 1, UserId = 1, IsAdmin = true}
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByCombination(request.GroupId, 1))
                .Returns(queryableMock);

            CanCreateMembershipQuery.Handler handler = new CanCreateMembershipQuery.Handler(unitOfWorkMock.Object, httpContextAccessorMock.Object);

            // Act
            bool canCreate = await handler.Handle(request);

            // Assert
            Assert.True(canCreate);
        }
    }
}
