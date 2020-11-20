using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Requests.Groups.Commands;
using Core.Domain.Dtos.Groups;
using Core.Domain.Resources.Groups;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
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
            CreateGroupDto model = new CreateGroupDto
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
                config.CreateMap<CreateGroupDto, CreateGroupCommand>();
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
    }
}
