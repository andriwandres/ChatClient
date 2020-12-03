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
    public class GetMembershipsByGroupQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetMembershipsByGroupQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<GroupMembership, GroupMembershipResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetMembershipByGroupQueryHandler_ShouldMemberships()
        {
            // Arrange
            GetMembershipsByGroupQuery request = new GetMembershipsByGroupQuery { GroupId = 1 };

            IEnumerable<GroupMembership> expectedMemberships = new []
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1 },
            };

            IQueryable<GroupMembership> queryableMock = expectedMemberships
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.GroupMemberships.GetByGroup(request.GroupId))
                .Returns(queryableMock);

            GetMembershipsByGroupQuery.Handler handler = new GetMembershipsByGroupQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            IEnumerable<GroupMembershipResource> actualMemberships = await handler.Handle(request);

            // Assert
            Assert.NotNull(actualMemberships);
            Assert.Equal(2, actualMemberships.Count());
        }
    }
}
