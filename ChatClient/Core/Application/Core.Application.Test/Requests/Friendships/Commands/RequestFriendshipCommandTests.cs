using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Commands
{
    public class RequestFriendshipCommandTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDateProvider> _dateProviderMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public RequestFriendshipCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _dateProviderMock = new Mock<IDateProvider>();

            _dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1));

            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Friendship, FriendshipResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task RequestFriendshipCommandHandler_ShouldReturnCreatedFriendship()
        {
            // Arrange
            RequestFriendshipCommand request = new RequestFriendshipCommand { AddresseeId = 2 };

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _unitOfWorkMock
                .Setup(m => m.Friendships.Add(It.IsAny<Friendship>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback<Friendship, CancellationToken>((f, _) => f.FriendshipId = 1);

            RequestFriendshipCommand.Handler handler = new RequestFriendshipCommand.Handler(_userProviderMock.Object, _unitOfWorkMock.Object, _dateProviderMock.Object, _mapperMock);

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
