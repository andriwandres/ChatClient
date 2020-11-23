using AutoMapper;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Requests.Friendships.Queries;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Friendships;
using Core.Domain.Entities;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Friendships;
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
    public class FriendshipControllerTests
    {
        [Fact]
        public async Task RequestFriendship_ShouldReturnBadRequestResult_WhenModelValidationFails()
        {
            // Arrange
            RequestFriendshipBody model = new RequestFriendshipBody { AddresseeId = -123 };

            FriendshipController controller = new FriendshipController(null, null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult<FriendshipResource> response = await controller.RequestFriendship(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task RegisterFriendship_ShouldReturnNotFoundResult_WhenAddresseeUserDoesNotExist()
        {
            // Arrange
            RequestFriendshipBody model = new RequestFriendshipBody { AddresseeId = 4142 };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            FriendshipController controller = new FriendshipController(mediatorMock.Object, null);

            // Act
            ActionResult<FriendshipResource> response = await controller.RequestFriendship(model);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task RegisterFriendship_ShouldReturnCreatedResult_WithCreatedResource()
        {
            // Arrange
            RequestFriendshipBody model = new RequestFriendshipBody { AddresseeId = 2 };

            FriendshipResource expectedFriendship = new FriendshipResource
            {
                AddresseeId = 2,
                RequesterId = 1,
                FriendshipId = 1
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<RequestFriendshipCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedFriendship);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<RequestFriendshipBody, RequestFriendshipCommand>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            FriendshipController controller = new FriendshipController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<FriendshipResource> response = await controller.RequestFriendship(model);

            // Assert
            CreatedAtActionResult result = Assert.IsType<CreatedAtActionResult>(response.Result);

            Assert.Equal(expectedFriendship, result.Value);
        }

        [Fact]
        public async Task GetFriendshipById_ShouldReturnNotFoundResult_WhenFriendshipIsNotFound()
        {
            // Arrange
            const int friendshipId = 632;

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetFriendshipByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((FriendshipResource) null);

            FriendshipController controller = new FriendshipController(mediatorMock.Object, null);

            // Act
            ActionResult<FriendshipResource> response = await controller.GetFriendshipById(friendshipId);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task GetFriendshipById_ShouldReturnOkResult_WhenFriendshipExists()
        {
            // Arrange
            const int friendshipId = 1;

            FriendshipResource expectedFriendship = new FriendshipResource {FriendshipId = friendshipId};

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetFriendshipByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedFriendship);

            FriendshipController controller = new FriendshipController(mediatorMock.Object, null);

            // Act
            ActionResult<FriendshipResource> response = await controller.GetFriendshipById(friendshipId);

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            FriendshipResource friendship = Assert.IsType<FriendshipResource>(result.Value);

            Assert.Equal(friendshipId, friendship.FriendshipId);
        }

        [Fact]
        public async Task UpdateFriendshipStatus_ShouldReturnBadRequestResult_WhenModelValidationFails()
        {
            // Arrange
            FriendshipController controller = new FriendshipController(null, null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult response = await controller.UpdateFriendshipStatus(0, null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task UpdateFriendshipStatus_ShouldReturnNotFound_WhenFriendshipDoesNotExist()
        {
            // Arrange
            const int friendshipId = 87921;

            UpdateFriendshipStatusBody model = new UpdateFriendshipStatusBody
            {
                FriendshipStatusId = FriendshipStatusId.Accepted
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<FriendshipExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            FriendshipController controller = new FriendshipController(mediatorMock.Object, null);

            // Act
            ActionResult response = await controller.UpdateFriendshipStatus(friendshipId, model);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task UpdateFriendshipStatus_ShouldReturnNoContentResult()
        {
            // Arrange
            const int friendshipId = 1;

            UpdateFriendshipStatusBody model = new UpdateFriendshipStatusBody
            {
                FriendshipStatusId = FriendshipStatusId.Accepted
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<FriendshipExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateFriendshipStatusCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Unit.Task);

            FriendshipController controller = new FriendshipController(mediatorMock.Object, null);

            // Act
            ActionResult response = await controller.UpdateFriendshipStatus(friendshipId, model);

            // Assert
            Assert.IsType<NoContentResult>(response);
            mediatorMock.Verify(m => m.Send(It.IsAny<UpdateFriendshipStatusCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
