using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries;

public class GetOwnFriendshipsQueryTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public GetOwnFriendshipsQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
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
    public async Task GetOwnFriendshipsQueryHandler_ShouldReturnOwnFriendships()
    {
        // Arrange
        List<Friendship> expectedFriendships = new()
        {
            new Friendship { FriendshipId = 1, RequesterId = 1, AddresseeId = 2 },
            new Friendship { FriendshipId = 2, RequesterId = 3, AddresseeId = 1 },
        };

        _unitOfWorkMock
            .Setup(m => m.Friendships.GetByUser(1))
            .ReturnsAsync(expectedFriendships);

        GetOwnFriendshipsQuery.Handler handler = new GetOwnFriendshipsQuery.Handler(_unitOfWorkMock.Object, _mapperMock, _userProviderMock.Object);

        // Act
        IEnumerable<FriendshipResource> friendships = await handler.Handle(new GetOwnFriendshipsQuery());

        // Assert
        Assert.Equal(2, friendships.Count());
    }
}