using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Commands
{
    public class RequestFriendshipCommandTests
    {
        [Fact]
        public async Task RequestFriendshipCommandHandler_ShouldReturnCreatedFriendship()
        {
            // Arrange
            RequestFriendshipCommand request = new RequestFriendshipCommand { AddresseeId = 2 };
            DateTime expectedDate = new DateTime(2020, 1, 1, 0, 0, 0);
            Claim expectedClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            Mock<IDateProvider> dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(expectedDate);

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedClaim);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            unitOfWorkMock
                .Setup(m => m.Friendships.Add(It.IsAny<Friendship>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback<Friendship, CancellationToken>((f, _) => f.FriendshipId = 1);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Friendship, FriendshipResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            RequestFriendshipCommand.RequestFriendshipCommandHandler handler =
                new RequestFriendshipCommand.RequestFriendshipCommandHandler(httpContextAccessorMock.Object, unitOfWorkMock.Object, dateProviderMock.Object, mapperMock);

            // Act
            FriendshipResource friendship = await handler.Handle(request);

            // Assert
            Assert.NotNull(friendship);
            Assert.NotEqual(0, friendship.FriendshipId);
            Assert.Equal(1, friendship.RequesterId);
            Assert.Equal(2, friendship.AddresseeId);
        }
    }
}
