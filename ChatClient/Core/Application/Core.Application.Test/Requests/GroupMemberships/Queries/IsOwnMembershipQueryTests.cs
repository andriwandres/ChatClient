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
    public class IsOwnMembershipQueryTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public IsOwnMembershipQueryTests()
        {
            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, 2.ToString());

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task IsOwnMembershipQueryHandler_ShouldReturnTrue_WhenMembershipIsOwn()
        {
            // Arrange
            IsOwnMembershipQuery request = new IsOwnMembershipQuery {GroupMembershipId = 2};

            IQueryable<GroupMembership> expectedMembership = new[]
            {
                new GroupMembership { GroupMembershipId = 1, UserId = 2, GroupId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(expectedMembership);

            IsOwnMembershipQuery.Handler handler = new IsOwnMembershipQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwn = await handler.Handle(request);

            // Assert
            Assert.True(isOwn);
        }

        [Fact]
        public async Task IsOwnMembershipQueryHandler_ShouldReturnFalse_WhenMembershipIsForeign()
        {
            // Arrange
            IsOwnMembershipQuery request = new IsOwnMembershipQuery { GroupMembershipId = 2 };

            IQueryable<GroupMembership> expectedMembership = new[]
            {
                new GroupMembership { GroupMembershipId = 1, UserId = 142, GroupId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(expectedMembership);

            IsOwnMembershipQuery.Handler handler = new IsOwnMembershipQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwn = await handler.Handle(request);

            // Assert
            Assert.False(isOwn);
        }
    }
}
