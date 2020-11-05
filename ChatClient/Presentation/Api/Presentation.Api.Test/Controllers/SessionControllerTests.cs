using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Requests.Session.Queries;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Session;
using Core.Domain.Dtos.Users;
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
            LoginCredentialsDto credentials = new LoginCredentialsDto();

            SessionController controller = new SessionController(null, null);

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
                .ReturnsAsync((AuthenticatedUser)null);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<LoginCredentialsDto, LoginQuery>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            SessionController controller = new SessionController(mediatorMock.Object, mapperMock);

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

            SessionController controller = new SessionController(mediatorMock.Object, mapperMock);

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
