﻿using Core.Application.Requests.Users.Queries;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Requests.Users.Commands;
using Core.Domain.Dtos.Users;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task RegisterUser_ShouldReturnBadRequestResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            RegisterUserDto credentials = new RegisterUserDto();

            UserController controller = new UserController(null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult response = await controller.RegisterUser(credentials);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnCreatedResult_WhenCredentialsAreValid()
        {
            // Arrange
            RegisterUserDto credentials = new RegisterUserDto
            {
                UserName = "myUsername",
                Email = "my@email.address",
                Password = "myPassword"
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult response = await controller.RegisterUser(credentials);

            // Assert
            CreatedAtActionResult result = Assert.IsType<CreatedAtActionResult>(response);

            Assert.NotEmpty(result.ActionName);
            Assert.NotEmpty(result.RouteValues);
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnBadRequestResult_WhenUserIdFailsValidation()
        {
            // Arrange
            const int userId = -3;

            UserController controller = new UserController(null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult<UserProfileResource> response = await controller.GetUserProfile(userId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            const int userId = 121234;

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUserProfileQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserProfileResource) null);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult<UserProfileResource> response = await controller.GetUserProfile(userId);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnOkResult_WhenUserExists()
        {
            // Arrange
            const int userId = 1;

            UserProfileResource expectedUserProfile = new UserProfileResource { UserId = 1 };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUserProfileQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUserProfile);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult<UserProfileResource> response = await controller.GetUserProfile(userId);

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            UserProfileResource profile = (UserProfileResource) result.Value;

            Assert.Equal(1, profile.UserId);
        }

        [Fact]
        public async Task EmailExists_ShouldReturnBadRequestResult_WhenEmailIsInvalid()
        {
            // Arrange
            UserController controller = new UserController(null);

            controller.ModelState.AddModelError("Email", "Required");

            // Act
            ActionResult response = await controller.EmailExists(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task EmailExists_ShouldReturnOkResult_WhenEmailExists()
        {
            // Arrange
            EmailExistsDto dto = new EmailExistsDto { Email = "test@test.test" };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<EmailExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult response = await controller.EmailExists(dto);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task EmailExists_ShouldReturnNotFoundResult_WhenEmailDoesNotExists()
        {
            // Arrange
            EmailExistsDto dto = new EmailExistsDto { Email = "not@existing.email" };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<EmailExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult response = await controller.EmailExists(dto);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnBadRequestResult_WhenUserNameIsInvalid()
        {
            // Arrange
            UserController controller = new UserController(null);

            controller.ModelState.AddModelError("UserName", "Required");

            // Act
            ActionResult response = await controller.UserNameExists(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnOkResult_WhenUserNameExists()
        {
            // Arrange
            UserNameExistsDto dto = new UserNameExistsDto { UserName = "myUserName" };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserNameExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult response = await controller.UserNameExists(dto);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnNotFoundResult_WhenUserNameDoesNotExists()
        {
            // Arrange
            UserNameExistsDto dto = new UserNameExistsDto { UserName = "myUserName" };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<UserNameExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult response = await controller.UserNameExists(dto);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnBadRequestResult_WhenUserCouldNotBeFound()
        {
            // Arrange
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<AuthenticateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthenticatedUserResource) null);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult<AuthenticatedUserResource> response = await controller.Authenticate();

            // Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUser_WhenAuthenticationWasSuccessful()
        {
            // Arrange
            AuthenticatedUserResource expectedUser = new AuthenticatedUserResource()
            {
                UserId = 1
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<AuthenticateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            UserController controller = new UserController(mediatorMock.Object);

            // Act
            ActionResult<AuthenticatedUserResource> response = await controller.Authenticate();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            AuthenticatedUserResource actualUser = (AuthenticatedUserResource) okResult.Value;

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
        }
    }
}
