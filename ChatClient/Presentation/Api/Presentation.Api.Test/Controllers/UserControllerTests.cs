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
    public class UserControllerTests
    {
        [Fact]
        public async Task EmailExists_ShouldReturnBadRequestResult_WhenEmailIsInvalid()
        {
            // Arrange
            UserController controller = new UserController(null, null);

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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<EmailExistsDto, EmailExistsQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            UserController controller = new UserController(mediatorMock.Object, mapperMock);

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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<EmailExistsDto, EmailExistsQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            UserController controller = new UserController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult response = await controller.EmailExists(dto);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnBadRequestResult_WhenUserNameIsInvalid()
        {
            // Arrange
            UserController controller = new UserController(null, null);

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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<UserNameExistsDto, UserNameExistsQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            UserController controller = new UserController(mediatorMock.Object, mapperMock);

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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<UserNameExistsDto, UserNameExistsQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            UserController controller = new UserController(mediatorMock.Object, mapperMock);

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
                .ReturnsAsync((AuthenticatedUser) null);

            UserController controller = new UserController(mediatorMock.Object, null);

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

            UserController controller = new UserController(mediatorMock.Object, null);

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

            UserController controller = new UserController(null, null);

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

            UserController controller = new UserController(mediatorMock.Object, mapperMock);

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

            UserController controller = new UserController(mediatorMock.Object, mapperMock);

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
