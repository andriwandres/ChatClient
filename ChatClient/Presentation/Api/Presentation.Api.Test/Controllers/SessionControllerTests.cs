using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Requests.Session.Queries;
using Core.Domain.Dtos.Session;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class SessionControllerTests
    {
        [Fact]
        public async Task Login_ShouldReturnBadRequestResult_WhenCredentialsFailValidation()
        {
            // Arrange
            LoginUserDto credentials = new LoginUserDto();

            SessionController controller = new SessionController(null, null);

            controller.ModelState.AddModelError("Credentials", "Required");

            // Act
            ActionResult<AuthenticatedUserResource> response = await controller.Login(credentials);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Login_ShouldReturnNotFoundResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            LoginUserDto credentials = new LoginUserDto
            {
                UserNameOrEmail = "myUsername",
                Password = "myPassword"
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthenticatedUserResource)null);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<LoginUserDto, LoginUserQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            SessionController controller = new SessionController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<AuthenticatedUserResource> response = await controller.Login(credentials);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            AuthenticatedUserResource expectedUser = new AuthenticatedUserResource() { UserId = 1 };

            LoginUserDto credentials = new LoginUserDto
            {
                UserNameOrEmail = "myUsername",
                Password = "myPassword"
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<LoginUserDto, LoginUserQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            SessionController controller = new SessionController(mediatorMock.Object, mapperMock);

            // Act
            ActionResult<AuthenticatedUserResource> response = await controller.Login(credentials);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            AuthenticatedUserResource actualUser = (AuthenticatedUserResource)okResult.Value;

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
        }
    }
}
