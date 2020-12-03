using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class CanCreateMembershipQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public CanCreateMembershipQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);
        }

        [Fact]
        public async Task CanCreateMembershipQueryHandler_ShouldReturnFalse_WhenUserIsNotPartOfGroup()
        {
            // Arrange
            CanCreateMembershipQuery request = new CanCreateMembershipQuery { GroupId = 1 };

            IQueryable<GroupMembership> expectedMemberships = Enumerable
                .Empty<GroupMembership>()
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByCombination(request.GroupId, 1))
                .Returns(expectedMemberships);
            
            CanCreateMembershipQuery.Handler handler = new CanCreateMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

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

            IEnumerable<GroupMembership> expectedMemberships = new[]
            {
                new GroupMembership {GroupMembershipId = 1, UserId = 1, IsAdmin = false}
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByCombination(request.GroupId, 1))
                .Returns(queryableMock);

            CanCreateMembershipQuery.Handler handler = new CanCreateMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

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

            IEnumerable<GroupMembership> expectedMemberships = new[]
            {
                new GroupMembership {GroupMembershipId = 1, UserId = 1, IsAdmin = true}
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByCombination(request.GroupId, 1))
                .Returns(queryableMock);

            CanCreateMembershipQuery.Handler handler = new CanCreateMembershipQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            bool canCreate = await handler.Handle(request);

            // Assert
            Assert.True(canCreate);
        }
    }
}
