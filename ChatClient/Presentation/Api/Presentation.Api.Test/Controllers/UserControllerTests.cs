using AutoMapper;
using Core.Application.Requests.Users.Commands;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Users;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Friendships;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Requests.Friendships.Queries;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Resources.Recipients;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateAccountBody, CreateAccountCommand>();
                config.CreateMap<CreateAccountBody, UserNameOrEmailExistsQuery>();
                config.CreateMap<EmailExistsQueryParams, EmailExistsQuery>();
                config.CreateMap<UserNameExistsQueryParams, UserNameExistsQuery>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }
        
        #region CreateUserAccount()
        
        [Fact]
        public async Task CreateUserAccount_ShouldReturnBadRequestResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            CreateAccountBody credentials = new CreateAccountBody();

            UserController controller = new UserController(null, null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult response = await controller.CreateAccount(credentials);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task CreateUserAccount_ShouldReturnForbiddenResult_WhenUserNameOrEmailAlreadyExists()
        {
            // Arrange
            CreateAccountBody credentials = new CreateAccountBody {UserName = "", Email = "", Password = ""};

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UserNameOrEmailExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);

            // Act
            ActionResult response = await controller.CreateAccount(credentials);

            // Assert
            ObjectResult result = Assert.IsType<ObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.Equal(StatusCodes.Status403Forbidden, error.StatusCode);
        }

        [Fact]
        public async Task CreateUserAccount_ShouldReturnCreatedResult_WhenCredentialsAreValid()
        {
            // Arrange
            CreateAccountBody credentials = new CreateAccountBody
            {
                UserName = "myUsername",
                Email = "my@email.address",
                Password = "myPassword"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UserNameOrEmailExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);

            // Act
            ActionResult response = await controller.CreateAccount(credentials);

            // Assert
            CreatedAtActionResult result = Assert.IsType<CreatedAtActionResult>(response);

            Assert.NotEmpty(result.ActionName);
            Assert.NotEmpty(result.RouteValues);
        }

        #endregion
        
        #region GetUserProfile()
        
        [Fact]
        public async Task GetUserProfile_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            const int userId = 121234;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUserProfileQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserProfileResource) null);

            UserController controller = new UserController(_mediatorMock.Object, null);

            // Act
            ActionResult<UserProfileResource> response = await controller.GetUserProfile(userId);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnOkResult_WhenUserExists()
        {
            // Arrange
            const int userId = 1;

            UserProfileResource expectedUserProfile = new UserProfileResource { UserId = 1 };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUserProfileQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUserProfile);

            UserController controller = new UserController(_mediatorMock.Object, null);

            // Act
            ActionResult<UserProfileResource> response = await controller.GetUserProfile(userId);

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            UserProfileResource profile = (UserProfileResource) result.Value;

            Assert.Equal(1, profile.UserId);
        }

        #endregion
        
        #region EmailExists()
        
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
            EmailExistsQueryParams queryParams = new EmailExistsQueryParams { Email = "test@test.test" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EmailExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);

            // Act
            ActionResult response = await controller.EmailExists(queryParams);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task EmailExists_ShouldReturnNotFoundResult_WhenEmailDoesNotExists()
        {
            // Arrange
            EmailExistsQueryParams queryParams = new EmailExistsQueryParams { Email = "not@existing.email" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EmailExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);

            // Act
            ActionResult response = await controller.EmailExists(queryParams);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        #endregion
        
        #region UserNameExists()
        
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
            UserNameExistsQueryParams queryParams = new UserNameExistsQueryParams { UserName = "myUserName" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UserNameExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);

            // Act
            ActionResult response = await controller.UserNameExists(queryParams);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnNotFoundResult_WhenUserNameDoesNotExists()
        {
            // Arrange
            UserNameExistsQueryParams queryParams = new UserNameExistsQueryParams { UserName = "myUserName" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UserNameExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);

            // Act
            ActionResult response = await controller.UserNameExists(queryParams);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        #endregion
        
        #region Authenticate()
        
        [Fact]
        public async Task Authenticate_ShouldReturnUser_WhenAuthenticationWasSuccessful()
        {
            // Arrange
            AuthenticatedUserResource expectedUser = new AuthenticatedUserResource()
            {
                UserId = 1
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AuthenticateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            UserController controller = new UserController(_mediatorMock.Object, null);

            // Act
            ActionResult<AuthenticatedUserResource> response = await controller.Authenticate();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            AuthenticatedUserResource actualUser = (AuthenticatedUserResource) okResult.Value;

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
        }

        #endregion
        
        #region GetOwnFriendships()
        
        [Fact]
        public async Task GetOwnFriendships_ShouldReturnFriendships()
        {
            // Arrange
            IEnumerable<FriendshipResource> expectedFriendships = new[]
            {
                new FriendshipResource {FriendshipId = 1}
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetOwnFriendshipsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedFriendships);

            UserController controller = new UserController(_mediatorMock.Object, null);

            // Act
            ActionResult<IEnumerable<FriendshipResource>> response = await controller.GetOwnFriendships();

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            IEnumerable<FriendshipResource> actualFriendships = (IEnumerable<FriendshipResource>) result.Value;

            Assert.Single(actualFriendships);
        }
        
        #endregion
        
        #region GetOwnRecipients()

        [Fact]
        public async Task GetOwnRecipients_ShouldReturnRecipients()
        {
            // Arrange
            IEnumerable<RecipientResource> expectedRecipients = new []
            {
                new RecipientResource { RecipientId = 1}
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetOwnRecipientsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRecipients);

            UserController controller = new UserController(_mediatorMock.Object, _mapperMock);
            
            // Act
            ActionResult<IEnumerable<RecipientResource>> response = await controller.GetOwnRecipients();

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }
        
        #endregion
    }
}
