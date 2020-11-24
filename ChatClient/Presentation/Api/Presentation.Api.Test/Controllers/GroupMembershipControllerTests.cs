using AutoMapper;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Requests.Groups.Queries;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.GroupMemberships;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.GroupMemberships;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class GroupMembershipControllerTests
    {
        [Fact]
        public async Task CreateMembership_ShouldReturnBadRequestResult_WhenModelValidationFails()
        {
            // Arrange
            GroupMembershipController controller = new GroupMembershipController(null, null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult<GroupMembershipResource> response = await controller.CreateMembership(new CreateMembershipBody());

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task CreateMembership_ShouldReturnNotFoundResult_WhenGroupDoesNotExist()
        {
            // Arrange
            CreateMembershipBody body = new CreateMembershipBody {GroupId = 6541, UserId = 1};

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            GroupMembershipController controller = new GroupMembershipController(mediatorMock.Object, null);

            // Act
            ActionResult<GroupMembershipResource> response = await controller.CreateMembership(body);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task CreateMembership_ShouldReturnNotFoundResult_WhenUserDoesNotExist()
        {
            // Arrange
            CreateMembershipBody body = new CreateMembershipBody { GroupId = 1, UserId = 751 };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            GroupMembershipController controller = new GroupMembershipController(mediatorMock.Object, null);

            // Act
            ActionResult<GroupMembershipResource> response = await controller.CreateMembership(body);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task CreateMembership_ShouldReturnForbiddenResult_WhenMembershipCombinationAlreadyExists()
        {
            // Arrange
            CreateMembershipBody body = new CreateMembershipBody { GroupId = 1, UserId = 1 };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<MembershipCombinationExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateMembershipBody, MembershipCombinationExistsQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GroupMembershipController controller = new GroupMembershipController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<GroupMembershipResource> response = await controller.CreateMembership(body);

            // Assert
            ObjectResult result = Assert.IsType<ObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status403Forbidden, error.StatusCode);
        }

        [Fact]
        public async Task CreateMembership_ShouldReturnCreatedResult_WhenMembershipWasCreated()
        {
            // Arrange
            CreateMembershipBody body = new CreateMembershipBody {GroupId = 1, UserId = 1};

            GroupMembershipResource expectedMembership = new GroupMembershipResource
            {
                GroupMembershipId = 1,
                GroupId = 1,
                UserId = 1
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GroupExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<MembershipCombinationExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateMembershipCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedMembership);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateMembershipBody, CreateMembershipCommand>();
                config.CreateMap<CreateMembershipBody, MembershipCombinationExistsQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GroupMembershipController controller = new GroupMembershipController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<GroupMembershipResource> response = await controller.CreateMembership(body);

            // Assert
            CreatedAtActionResult result = Assert.IsType<CreatedAtActionResult>(response.Result);

            GroupMembershipResource createdMembership = Assert.IsType<GroupMembershipResource>(result.Value);

            Assert.NotNull(createdMembership);
            Assert.Equal(expectedMembership, createdMembership);
        }
    }
}
