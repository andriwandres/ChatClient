using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Groups.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Queries
{
    public class GetGroupByIdQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetGroupByIdQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<Group, GroupResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetGroupByIdHandler_ShouldReturnNull_WhenGroupDoesNotExist()
        {
            // Arrange
            GetGroupByIdQuery request = new() {GroupId = 3891};

            _unitOfWorkMock
                .Setup(m => m.Groups.GetByIdAsync(request.GroupId))
                .ReturnsAsync(null as Group);

            GetGroupByIdQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

            // Act
            GroupResource group = await handler.Handle(request);

            // Assert
            Assert.Null(group);
        }

        [Fact]
        public async Task GetGroupByIdHandler_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            GetGroupByIdQuery request = new() { GroupId = 1 };

            Group expectedGroups = new() { GroupId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Groups.GetByIdAsync(request.GroupId))
                .ReturnsAsync(expectedGroups);

            GetGroupByIdQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object);

            // Act
            GroupResource group = await handler.Handle(request);

            // Assert
            Assert.NotNull(group);
            Assert.Equal(request.GroupId, group.GroupId);
        }
    }
}
