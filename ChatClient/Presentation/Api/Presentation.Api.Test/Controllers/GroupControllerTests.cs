﻿using AutoMapper;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Requests.Groups.Commands;
using Core.Application.Requests.Groups.Queries;
using Core.Domain.Dtos.Groups;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.GroupMemberships;
using Core.Domain.Resources.Groups;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Presentation.Api.Test.Controllers;

public class GroupControllerTests
{
    [Fact]
    public async Task CreateGroup_ShouldReturnBadRequestResult_WhenModelValidationFails()
    {
        // Arrange
        GroupController controller = new GroupController(null, null);

        controller.ModelState.AddModelError("", "");

        // Act
        ActionResult<GroupResource> response = await controller.CreateGroup(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Fact]
    public async Task CreateGroup_ShouldReturnCreatedResult_WhenGroupIsCreated()
    {
        // Arrange
        CreateGroupBody model = new CreateGroupBody
        {
            Name = "Some group name",
            Description = "Some group description"
        };

        GroupResource expectedGroup = new GroupResource
        {
            GroupId = 1,
            Name = model.Name,
            Description = model.Description
        };

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateGroupCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedGroup);

        MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<CreateGroupBody, CreateGroupCommand>();
        });

        IMapper mapperMock = mapperConfiguration.CreateMapper();

        GroupController controller = new GroupController(mediatorMock.Object, mapperMock);

        // Act
        ActionResult<GroupResource> response = await controller.CreateGroup(model);

        // Assert
        CreatedAtActionResult result = Assert.IsType<CreatedAtActionResult>(response.Result);

        GroupResource actualGroup = Assert.IsType<GroupResource>(result.Value);

        Assert.NotNull(actualGroup);
        Assert.Equal(1, actualGroup.GroupId);

        mediatorMock.Verify(m => m.Send(It.IsAny<CreateGroupCommand>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task GetGroupById_ShouldReturnNotFoundResult_WhenGroupDoesNotExist()
    {
        // Arrange
        const int groupId = 8821;

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetGroupByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GroupResource) null);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult<GroupResource> response = await controller.GetGroupById(groupId);

        // Assert
        NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

        ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

        Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
    }

    [Fact]
    public async Task GetGroupById_ShouldReturnGroup_WhenGroupExists()
    {
        // Arrange
        const int groupId = 1;

        GroupResource expectedGroup = new GroupResource
        {
            GroupId = groupId
        };

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetGroupByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedGroup);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult<GroupResource> response = await controller.GetGroupById(groupId);

        // Assert
        OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

        GroupResource actualGroup = Assert.IsType<GroupResource>(result.Value);

        Assert.NotNull(actualGroup);
        Assert.Equal(groupId, actualGroup.GroupId);
    }

    [Fact]
    public async Task UpdateGroup_ShouldReturnBadRequestResult_WhenModelValidationFails()
    {
        // Arrange
        GroupController controller = new GroupController(null, null);

        controller.ModelState.AddModelError("", "");

        // Act
        ActionResult response = await controller.UpdateGroup(0, null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public async Task UpdateGroup_ShouldReturnNotFoundResult_WhenGroupDoesNotExist()
    {
        // Arrange
        const int groupId = 15453;

        UpdateGroupBody model = new UpdateGroupBody
        {
            Name = "Some updated name",
            Description = "Some updated description"
        };

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult response = await controller.UpdateGroup(groupId, model);

        // Assert
        NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response);

        ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

        Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
    }

    [Fact]
    public async Task UpdateGroup_ShouldReturnUpdateGroup_WhenGroupExists()
    {
        // Arrange
        const int groupId = 1;

        UpdateGroupBody model = new UpdateGroupBody
        {
            Name = "Some updated name",
            Description = "Some updated description"
        };

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult response = await controller.UpdateGroup(groupId, model);

        // Assert
        Assert.IsType<NoContentResult>(response);

        mediatorMock.Verify(m => m.Send(It.IsAny<UpdateGroupCommand>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task DeleteGroup_ShouldReturnNotFoundResult_WhenGroupDoesNotExist()
    {
        // Arrange
        const int groupId = 3894;

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult response = await controller.DeleteGroup(groupId);

        // Assert
        NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response);

        ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

        Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
    }

    [Fact]
    public async Task DeleteGroup_ShouldReturnDeleteGroup_WhenGroupExists()
    {
        // Arrange
        const int groupId = 1;

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult response = await controller.DeleteGroup(groupId);

        // Assert
        Assert.IsType<NoContentResult>(response);

        mediatorMock.Verify(m => m.Send(It.IsAny<DeleteGroupCommand>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task GetMembershipsByGroup_ShouldReturnNotFoundResult_WhenGroupDoesNotExist()
    {
        // Arrange
        const int groupId = 842;

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult<IEnumerable<GroupMembershipResource>> response = await controller.GetMembershipsByGroup(groupId);

        // Assert
        NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

        ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

        Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
    }

    [Fact]
    public async Task GetMembershipsByGroup_ShouldGetMemberships_WhenGroupExists()
    {
        // Arrange
        const int groupId = 1;

        IEnumerable<GroupMembershipResource> expectedMemberships = new []
        {
            new GroupMembershipResource { GroupMembershipId = 1, GroupId = 1 }
        };

        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetMembershipsByGroupQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedMemberships);


        GroupController controller = new GroupController(mediatorMock.Object, null);

        // Act
        ActionResult<IEnumerable<GroupMembershipResource>> response = await controller.GetMembershipsByGroup(groupId);

        // Assert
        OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

        IEnumerable<GroupMembershipResource> actualMemberships = (IEnumerable<GroupMembershipResource>) result.Value;

        Assert.NotNull(actualMemberships);
        Assert.Single(actualMemberships);
    }
}