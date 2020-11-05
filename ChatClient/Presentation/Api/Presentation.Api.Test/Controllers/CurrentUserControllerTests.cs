using Core.Application.Requests.Users.Queries;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Dtos.Users;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class CurrentUserControllerTests
    {
        [Fact]
        public async Task Authenticate_ShouldReturnBadRequestResult_WhenUserCouldNotBeFound()
        {
            // Arrange
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<AuthenticateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthenticatedUser) null);

            CurrentUserController controller = new CurrentUserController(mediatorMock.Object, null);

            // Act
            ActionResult<AuthenticatedUser> response = await controller.Authenticate();

            // Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUser_WhenAuthenticationWasSuccessful()
        {
            // Arrange
            AuthenticatedUser expectedUser = new AuthenticatedUser()
            {
                UserId = 1
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<AuthenticateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            CurrentUserController controller = new CurrentUserController(mediatorMock.Object, null);

            // Act
            ActionResult<AuthenticatedUser> response = await controller.Authenticate();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            AuthenticatedUser actualUser = (AuthenticatedUser) okResult.Value;

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequestResult_WhenCredentialsFailValidation()
        {
            // Arrange
            LoginCredentialsDto credentials = new LoginCredentialsDto();

            CurrentUserController controller = new CurrentUserController(null, null);

            controller.ModelState.AddModelError("Credentials", "Required");

            // Act
            ActionResult<AuthenticatedUser> response = await controller.Login(credentials);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Login_ShouldReturnNotFoundResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            LoginCredentialsDto credentials = new LoginCredentialsDto
            {
                UserNameOrEmail = "myUsername",
                Password = "myPassword"
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthenticatedUser) null);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<LoginCredentialsDto, LoginQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            CurrentUserController controller = new CurrentUserController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<AuthenticatedUser> response = await controller.Login(credentials);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            AuthenticatedUser expectedUser = new AuthenticatedUser() { UserId = 1 };

            LoginCredentialsDto credentials = new LoginCredentialsDto
            {
                UserNameOrEmail = "myUsername",
                Password = "myPassword"
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<LoginCredentialsDto, LoginQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            CurrentUserController controller = new CurrentUserController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<AuthenticatedUser> response = await controller.Login(credentials);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            AuthenticatedUser actualUser = (AuthenticatedUser)okResult.Value;

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
        }
    }
}
