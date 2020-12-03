using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.GroupMemberships;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Queries
{
    public class GetMembershipByIdQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetMembershipByIdQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<GroupMembership, GroupMembershipResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetMembershipByIdQueryHandler_ShouldReturnNull_WhenMembershipDoesNotExist()
        {
            // Arrange
            GetMembershipByIdQuery request = new GetMembershipByIdQuery { GroupMembershipId = 1 };

            IQueryable<GroupMembership> expectedMemberships = Enumerable
                .Empty<GroupMembership>()
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(expectedMemberships);

            GetMembershipByIdQuery.Handler handler = new GetMembershipByIdQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            GroupMembershipResource membership = await handler.Handle(request);

            // Assert
            Assert.Null(membership);
        }

        [Fact]
        public async Task GetMembershipByIdQueryHandler_ShouldReturnMembership_WhenMembershipExists()
        {
            // Arrange
            GetMembershipByIdQuery request = new GetMembershipByIdQuery { GroupMembershipId = 1 };

            IEnumerable<GroupMembership> expectedMemberships = new[]
            {
                new GroupMembership {GroupMembershipId = 1}
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetById(request.GroupMembershipId))
                .Returns(queryableMock);

            GetMembershipByIdQuery.Handler handler = new GetMembershipByIdQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            GroupMembershipResource membership = await handler.Handle(request);

            // Assert
            Assert.NotNull(membership);
            Assert.Equal(request.GroupMembershipId, membership.GroupMembershipId);
        }
    }
}
