﻿using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Core.Domain.Entities;
using Moq;
using System.Threading.Tasks;
using Core.Domain.ViewModels.Friendships;
using Xunit;

namespace Core.Application.Test.Requests.Friendships.Queries;

public class GetFriendshipByIdQueryTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public GetFriendshipByIdQueryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        MapperConfiguration mapperConfiguration = new(config =>
        {
            config.CreateMap<Friendship, FriendshipViewModel>();
        });

        _mapperMock = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetFriendshipByIdQueryHandler_ShouldReturnNull_WhenFriendshipIsNotFound()
    {
        // Arrange
        GetFriendshipByIdQuery request = new() { FriendshipId = 2151 };
            
        _unitOfWorkMock
            .Setup(m => m.Friendships.GetByIdAsync(request.FriendshipId))
            .ReturnsAsync(null as Friendship);

        GetFriendshipByIdQuery.Handler handler = new(_unitOfWorkMock.Object, _mapperMock);

        // Act
        FriendshipViewModel friendship = await handler.Handle(request);

        // Assert
        Assert.Null(friendship);
    }

    [Fact]
    public async Task GetFriendshipByIdQueryHandler_ShouldReturnFriendship_WhenFriendshipExists()
    {
        // Arrange
        GetFriendshipByIdQuery request = new() { FriendshipId = 1 };

        Friendship expectedFriendship = new() { FriendshipId = 1 };

        _unitOfWorkMock
            .Setup(m => m.Friendships.GetByIdAsync(request.FriendshipId))
            .ReturnsAsync(expectedFriendship);

        GetFriendshipByIdQuery.Handler handler = new(_unitOfWorkMock.Object, _mapperMock);

        // Act
        FriendshipViewModel friendship = await handler.Handle(request);

        // Assert
        Assert.NotNull(friendship);
        Assert.Equal(1, friendship.FriendshipId);
    }
}