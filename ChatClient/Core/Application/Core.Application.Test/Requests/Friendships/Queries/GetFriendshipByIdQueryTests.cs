using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries
{
    public class GetFriendshipByIdQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetFriendshipByIdQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Friendship, FriendshipResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetFriendshipByIdQueryHandler_ShouldReturnNull_WhenFriendshipIsNotFound()
        {
            // Arrange
            GetFriendshipByIdQuery request = new GetFriendshipByIdQuery { FriendshipId = 2151 };

            Mock<IQueryable<Friendship>> expectedFriendships = Enumerable
                .Empty<Friendship>()
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Friendships.GetById(request.FriendshipId))
                .Returns(expectedFriendships.Object);

            GetFriendshipByIdQuery.Handler handler = new GetFriendshipByIdQuery.Handler(_unitOfWorkMock.Object, _mapperMock);

            // Act
            FriendshipResource friendship = await handler.Handle(request);

            // Assert
            Assert.Null(friendship);
        }

        [Fact]
        public async Task GetFriendshipByIdQueryHandler_ShouldReturnFriendship_WhenFriendshipExists()
        {
            // Arrange
            GetFriendshipByIdQuery request = new GetFriendshipByIdQuery { FriendshipId = 1 };

            IEnumerable<Friendship> expectedFriendships = new[]
            {
                new Friendship { FriendshipId = 1 }
            };

            Mock<IQueryable<Friendship>> friendshipQueryableMock = expectedFriendships
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Friendships.GetById(request.FriendshipId))
                .Returns(friendshipQueryableMock.Object);

            GetFriendshipByIdQuery.Handler handler = new GetFriendshipByIdQuery.Handler(_unitOfWorkMock.Object, _mapperMock);

            // Act
            FriendshipResource friendship = await handler.Handle(request);

            // Assert
            Assert.NotNull(friendship);
            Assert.Equal(1, friendship.FriendshipId);
        }
    }
}
