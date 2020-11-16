using System.Collections.Generic;
using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries
{
    public class GetFriendshipByIdQueryTests
    {
        [Fact]
        public async Task GetFriendshipByIdQueryHandler_ShouldReturnNull_WhenFriendshipIsNotFound()
        {
            // Arrange
            GetFriendshipByIdQuery request = new GetFriendshipByIdQuery { FriendshipId = 2151 };

            Mock<IQueryable<Friendship>> expectedFriendships = Enumerable
                .Empty<Friendship>()
                .AsQueryable()
                .BuildMock();

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.GetById(request.FriendshipId))
                .Returns(expectedFriendships.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Friendship, FriendshipResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetFriendshipByIdQuery.GetFriendshipByIdQueryHandler handler =
                new GetFriendshipByIdQuery.GetFriendshipByIdQueryHandler(unitOfWorkMock.Object, mapperMock);

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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.GetById(request.FriendshipId))
                .Returns(friendshipQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Friendship, FriendshipResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetFriendshipByIdQuery.GetFriendshipByIdQueryHandler handler =
                new GetFriendshipByIdQuery.GetFriendshipByIdQueryHandler(unitOfWorkMock.Object, mapperMock);

            // Act
            FriendshipResource friendship = await handler.Handle(request);

            // Assert
            Assert.NotNull(friendship);
            Assert.Equal(1, friendship.FriendshipId);
        }
    }
}
