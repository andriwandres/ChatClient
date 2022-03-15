using Core.Application.Database;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.GroupMemberships.Commands;

public class DeleteMembershipCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserProvider> _userProviderMock;

    public DeleteMembershipCommandTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _userProviderMock = new Mock<IUserProvider>();
        _userProviderMock
            .Setup(m => m.GetCurrentUserId())
            .Returns(1);
    }

    [Fact]
    public async Task DeleteMembershipCommandHandler_ShouldDeleteMembership_WhenItIsForeignMembership()
    {
        // Arrange
        DeleteMembershipCommand request = new() { GroupMembershipId = 1 };
        GroupMembership expectedMembership = new() {GroupMembershipId = 1, UserId = 2};

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByIdAsync(1))
            .ReturnsAsync(expectedMembership);

        GroupMembership passedMembership = null;

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.Delete(It.IsAny<GroupMembership>()))
            .Callback<GroupMembership>(gm => passedMembership = gm);

        _unitOfWorkMock
            .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        DeleteMembershipCommand.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        await handler.Handle(request);

        // Assert
        _unitOfWorkMock.Verify(m => m.GroupMemberships.Delete(It.IsAny<GroupMembership>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

        Assert.NotNull(passedMembership);
        Assert.Equal(request.GroupMembershipId, passedMembership.GroupMembershipId);
    }

    [Fact]
    public async Task DeleteMembershipCommandHandler_ShouldDeleteMembership_AndDeleteGroup_WhenTheUserLeavesByHimselfAndThereAreNoOtherMembersInTheGroup()
    {
        // Arrange
        DeleteMembershipCommand request = new() { GroupMembershipId = 1 };
            
        Group expectedGroup = new() { GroupId = 1 };
        GroupMembership expectedMembership = new() { GroupMembershipId = 1, UserId = 1, GroupId = 1 };
        List<GroupMembership> expectedMembershipsOfGroup = new()
        {
            new GroupMembership { GroupMembershipId = 1, UserId = 1, GroupId = 1 },
        };

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByIdAsync(1))
            .ReturnsAsync(expectedMembership);

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByGroup(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedMembershipsOfGroup);

        _unitOfWorkMock
            .Setup(m => m.Groups.GetByIdAsync(1))
            .ReturnsAsync(expectedGroup);

        Group passedUpdateGroup = null;

        _unitOfWorkMock
            .Setup(m => m.Groups.Update(It.IsAny<Group>()))
            .Callback<Group>(g => passedUpdateGroup = g);

        GroupMembership passedDeleteMembership = null;

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.Delete(It.IsAny<GroupMembership>()))
            .Callback<GroupMembership>(gm => passedDeleteMembership = gm);

        _unitOfWorkMock
            .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        DeleteMembershipCommand.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        await handler.Handle(request);

        // Assert
        _unitOfWorkMock.Verify(m => m.Groups.Update(It.IsAny<Group>()));

        Assert.NotNull(passedUpdateGroup);
        Assert.Equal(1, passedUpdateGroup.GroupId);
        Assert.True(passedUpdateGroup.IsDeleted);

        _unitOfWorkMock.Verify(m => m.GroupMemberships.Delete(It.IsAny<GroupMembership>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

        Assert.NotNull(passedDeleteMembership);
        Assert.Equal(request.GroupMembershipId, passedDeleteMembership.GroupMembershipId);
    }

    [Fact]
    public async Task DeleteMembershipCommandHandler_ShouldDeleteMembership_AndAssignNewAdministrator_WhenTheUserLeavesByHimselfAndThereAreNoOtherAdministrators()
    {
        // Arrange
        DeleteMembershipCommand request = new() { GroupMembershipId = 1 };
        GroupMembership expectedMembership = new() { GroupMembershipId = 1, UserId = 1, GroupId = 1 };
        List<GroupMembership> expectedMembershipsOfGroup = new()
        {
            new GroupMembership {GroupMembershipId = 1, UserId = 1, GroupId = 1, Created = new DateTime(2020, 1, 1) },
            new GroupMembership {GroupMembershipId = 2, UserId = 2, GroupId = 1, Created = new DateTime(2020, 1, 2) },
            new GroupMembership {GroupMembershipId = 3, UserId = 3, GroupId = 1, Created = new DateTime(2020, 1, 3) }
        };
            
        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByIdAsync(1))
            .ReturnsAsync(expectedMembership);

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.GetByGroup(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedMembershipsOfGroup);

        GroupMembership passedUpdateMembership = null;

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()))
            .Callback<GroupMembership>(gm => passedUpdateMembership = gm);

        GroupMembership passedDeleteMembership = null;

        _unitOfWorkMock
            .Setup(m => m.GroupMemberships.Delete(It.IsAny<GroupMembership>()))
            .Callback<GroupMembership>(gm => passedDeleteMembership = gm);

        _unitOfWorkMock
            .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        DeleteMembershipCommand.Handler handler = new(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        await handler.Handle(request);

        // Assert
        _unitOfWorkMock.Verify(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()));

        _unitOfWorkMock.Verify(m => m.GroupMemberships.Delete(It.IsAny<GroupMembership>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

        Assert.NotNull(passedUpdateMembership);
        Assert.Equal(2, passedUpdateMembership.UserId);
        Assert.True(passedUpdateMembership.IsAdmin);

        Assert.NotNull(passedDeleteMembership);
        Assert.Equal(request.GroupMembershipId, passedDeleteMembership.GroupMembershipId);
    }
}