using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries
{
    public class GetOwnFriendshipsQueryTests
    {
        [Fact]
        public async Task GetOwnFriendshipsQueryHandler_ShouldReturnOwnFriendships()
        {
            // Arrange
            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");
            
            IEnumerable<Friendship> expectedFriendships = new []
            {
                new Friendship { FriendshipId = 1, RequesterId = 1, AddresseeId = 2 },
                new Friendship { FriendshipId = 2, RequesterId = 3, AddresseeId = 1 },
            };

            Mock<IQueryable<Friendship>> friendshipQueryableMock = expectedFriendships
                .AsQueryable()
                .BuildMock();

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Friendships.GetByUser(1))
                .Returns(friendshipQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Friendship, FriendshipResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetOwnFriendshipsQuery.GetOwnFriendshipsQueryHandler handler =
                new GetOwnFriendshipsQuery.GetOwnFriendshipsQueryHandler(unitOfWorkMock.Object, httpContextAccessorMock.Object, mapperMock);

            // Act
            IEnumerable<FriendshipResource> friendships = await handler.Handle(new GetOwnFriendshipsQuery());

            // Assert
            Assert.Equal(2, friendships.Count());
        }
    }
}
