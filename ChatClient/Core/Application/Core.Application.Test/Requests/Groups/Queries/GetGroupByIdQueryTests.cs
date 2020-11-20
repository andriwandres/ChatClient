using System.Collections.Generic;
using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Groups.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Queries
{
    public class GetGroupByIdQueryTests
    {
        private readonly IMapper _mapperMock;

        public GetGroupByIdQueryTests()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Group, GroupResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetGroupByIdHandler_ShouldReturnNull_WhenGroupDoesNotExist()
        {
            // Arrange
            GetGroupByIdQuery request = new GetGroupByIdQuery {GroupId = 3891};

            IQueryable<Group> emptyQueryable = Enumerable
                .Empty<Group>()
                .AsQueryable()
                .BuildMock()
                .Object;

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Groups.GetById(request.GroupId))
                .Returns(emptyQueryable);

            GetGroupByIdQuery.Handler handler = new GetGroupByIdQuery.Handler(_mapperMock, unitOfWorkMock.Object);

            // Act
            GroupResource group = await handler.Handle(request);

            // Assert
            Assert.Null(group);
        }

        [Fact]
        public async Task GetGroupByIdHandler_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            GetGroupByIdQuery request = new GetGroupByIdQuery { GroupId = 1 };

            IEnumerable<Group> expectedGroups = new[]
            {
                new Group {GroupId = 1}
            };

            IQueryable<Group> queryableMock = expectedGroups
                .AsQueryable()
                .BuildMock()
                .Object;

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Groups.GetById(request.GroupId))
                .Returns(queryableMock);

            GetGroupByIdQuery.Handler handler = new GetGroupByIdQuery.Handler(_mapperMock, unitOfWorkMock.Object);

            // Act
            GroupResource group = await handler.Handle(request);

            // Assert
            Assert.NotNull(group);
            Assert.Equal(request.GroupId, group.GroupId);
        }
    }
}
