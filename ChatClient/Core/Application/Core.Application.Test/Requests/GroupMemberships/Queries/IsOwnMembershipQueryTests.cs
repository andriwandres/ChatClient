using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class IsOwnMembershipQueryTests
    {
        private readonly Mock<IUserProvider> _userProviderMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public IsOwnMembershipQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);
        }

        [Fact]
        public async Task IsOwnMembershipQueryHandler_ShouldReturnTrue_WhenMembershipIsOwn()
        {
            // Arrange
            IsOwnMembershipQuery request = new IsOwnMembershipQuery {GroupMembershipId = 2};

            IQueryable<GroupMembership> expectedMembership = new[]
            {
                new GroupMembership { GroupMembershipId = 2, UserId = 1, GroupId = 2 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(expectedMembership);

            IsOwnMembershipQuery.Handler handler = new IsOwnMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

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
                new GroupMembership { GroupMembershipId = 2, UserId = 142, GroupId = 2 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(expectedMembership);

            IsOwnMembershipQuery.Handler handler = new IsOwnMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            bool isOwn = await handler.Handle(request);

            // Assert
            Assert.False(isOwn);
        }
    }
}
