using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Groups.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Groups.Commands;

public class CreateGroupCommandTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDateProvider> _dateProviderMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public CreateGroupCommandTests()
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
            config.CreateMap<Group, GroupResource>();
        });

        _mapperMock = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task CreateGroupCommandHandler_ShouldReturnCreatedGroup()
    {
        // Arrange
        CreateGroupCommand request = new CreateGroupCommand
        {
            Name = "Some group name",
            Description = "Some group description"
        };

        _unitOfWorkMock
            .Setup(m => m.Groups.Add(It.IsAny<Group>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.Add(It.IsAny<GroupMembership>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        CreateGroupCommand.Handler handler = 
            new CreateGroupCommand.Handler(_mapperMock, _unitOfWorkMock.Object, _dateProviderMock.Object, _userProviderMock.Object);

        // Act
        GroupResource group = await handler.Handle(request);

        // Assert
        Assert.NotNull(group);

        _unitOfWorkMock.Verify(m => m.Groups.Add(It.IsAny<Group>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.GroupMemberships.Add(It.IsAny<GroupMembership>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}