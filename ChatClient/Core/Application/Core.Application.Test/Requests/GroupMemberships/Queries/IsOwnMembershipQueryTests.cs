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
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public IsOwnMembershipQueryTests()
        {
            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, 1.ToString());

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task IsOwnMembershipQueryHandler_ShouldReturnTrue_WhenMembershipIsOwn()
        {
            // Arrange
            IsOwnMembershipQuery request = new IsOwnMembershipQuery {GroupMembershipId = 1};

            IQueryable<GroupMembership> expectedMembership = new[]
            {
                new GroupMembership { GroupMembershipId = 1, UserId = 1, GroupId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWork
                .Setup(m => m.GroupMemberships.GetById(1))
                .Returns(expectedMembership);

            IsOwnMembershipQuery.Handler handler = new IsOwnMembershipQuery.Handler(_unitOfWork.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwn = await handler.Handle(request);

            // Assert
            Assert.True(isOwn);
        }

        [Fact]
        public async Task IsOwnMembershipQueryHandler_ShouldReturnFalse_WhenMembershipIsForeign()
        {
            // Arrange
            IsOwnMembershipQuery request = new IsOwnMembershipQuery { GroupMembershipId = 1 };

            IQueryable<GroupMembership> expectedMembership = new[]
            {
                new GroupMembership { GroupMembershipId = 1, UserId = 142, GroupId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWork
                .Setup(m => m.GroupMemberships.GetById(1))
                .Returns(expectedMembership);

            IsOwnMembershipQuery.Handler handler = new IsOwnMembershipQuery.Handler(_unitOfWork.Object, _httpContextAccessorMock.Object);

            // Act
            bool isOwn = await handler.Handle(request);

            // Assert
            Assert.True(isOwn);
        }
    }
}
