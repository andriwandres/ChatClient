using AutoMapper;
using Core.Application.Requests.Messages.Commands;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Dtos.Messages;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Messages;
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
    public class MessageControllerTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;

        public MessageControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<SendMessageBody, SendMessageCommand>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        #region SendMessage()

        [Fact]
        public async Task SendMessage_ShouldReturnBadRequestResult_WhenModelValidationFails()
        {
            // Arrange
            SendMessageBody body = new SendMessageBody();

            MessageController controller = new MessageController(null, null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult response = await controller.SendMessage(body);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task SendMessage_ShouldReturnNotFoundResult_WhenRecipientDoesNotExist()
        {
            // Arrange
            SendMessageBody body = new SendMessageBody
            {
                RecipientId = 4314, HtmlContent = "hello world"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.SendMessage(body);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task SendMessage_ShouldReturnForbiddenResult_WhenUserTriesMessagingHimself()
        {
            // Arrange
            SendMessageBody body = new SendMessageBody
            {
                RecipientId = 1,
                HtmlContent = "hello world"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<IsOwnRecipientQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.SendMessage(body);

            // Assert
            ObjectResult result = Assert.IsType<ObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status403Forbidden, error.StatusCode);
        }

        [Fact]
        public async Task SendMessage_ShouldReturnNotFoundResult_WhenParentMessageDoesNotExist()
        {
            // Arrange
            SendMessageBody body = new SendMessageBody
            {
                RecipientId = 1,
                ParentId = 381,
                HtmlContent = "hello world"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<IsOwnRecipientQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.SendMessage(body);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task SendMessage_ShouldReturnForbiddenResult_WhenUserIsNotAllowedToAccessParentMessage()
        {
            // Arrange
            SendMessageBody body = new SendMessageBody
            {
                RecipientId = 1,
                ParentId = 14,
                HtmlContent = "hello world"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<IsOwnRecipientQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CanAccessMessageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.SendMessage(body);

            // Assert
            ObjectResult result = Assert.IsType<ObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status403Forbidden, error.StatusCode);
        }

        [Fact]
        public async Task SendMessage_ShouldReturnCreatedResult_WhenMessageHasBeenSent()
        {
            // Arrange
            SendMessageBody body = new SendMessageBody
            {
                RecipientId = 1,
                ParentId = 1,
                HtmlContent = "hello world"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<IsOwnRecipientQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CanAccessMessageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SendMessageCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            MessageController controller = new MessageController(_mapperMock, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.SendMessage(body);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
        }

        #endregion

        #region GetMessageById()

        [Fact]
        public async Task GetMessageById_ShouldReturnNotFoundResult_WhenMessageDoesNotExist()
        {
            // Arrange
            const int messageId = 431;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult<MessageResource> response = await controller.GetMessageById(messageId);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task GetMessageById_ShouldReturnForbiddenResult_WhenUserIsNotPermittedToAccessMessage()
        {
            // Arrange
            const int messageId = 1;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CanAccessMessageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult<MessageResource> response = await controller.GetMessageById(messageId);

            // Assert
            ObjectResult result = Assert.IsType<ObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status403Forbidden, error.StatusCode);
        }

        [Fact]
        public async Task GetMessageById_ShouldReturnMessage_WhenIdMatchesAndUserIsPermittedToAccess()
        {
            // Arrange
            const int messageId = 1;

            MessageResource expectedMessage = new MessageResource { MessageId = 1 };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CanAccessMessageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetMessageByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedMessage);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult<MessageResource> response = await controller.GetMessageById(messageId);

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            MessageResource message = Assert.IsType<MessageResource>(result.Value);

            Assert.NotNull(message);
            Assert.Equal(messageId, message.MessageId);
        }

        #endregion

        #region EditMessage()

        [Fact]
        public async Task EditMessage_ShouldReturnBadRequestResult_WhenModelValidationFails()
        {
            // Arrange
            MessageController controller = new MessageController(null, null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult response = await controller.EditMessage(1, new EditMessageBody());

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task EditMessage_ShouldReturnNotFoundResult_WhenMessageDoesNotExist()
        {
            // Arrange
            const int messageId = 1;
            EditMessageBody body = new EditMessageBody { HtmlContent = "<p>hello world</p>"};

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.EditMessage(messageId, body);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task EditMessage_ShouldReturnForbiddenResult_WhenUserIsNotTheAuthorOfTheMessage()
        {
            // Arrange
            const int messageId = 1;
            EditMessageBody body = new EditMessageBody { HtmlContent = "<p>hello world</p>" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<IsAuthorOfMessageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.EditMessage(messageId, body);

            // Assert
            ObjectResult result = Assert.IsType<ObjectResult>(response);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status403Forbidden, error.StatusCode);
        }

        [Fact]
        public async Task EditMessage_ShouldUpdateTheMessagesContent()
        {
            // Arrange
            const int messageId = 1;
            EditMessageBody body = new EditMessageBody { HtmlContent = "<p>hello world</p>" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<MessageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<IsAuthorOfMessageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EditMessageCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            MessageController controller = new MessageController(null, _mediatorMock.Object);

            // Act
            ActionResult response = await controller.EditMessage(messageId, body);

            // Assert
            Assert.IsType<NoContentResult>(response);

            _mediatorMock.Verify(m => m.Send(It.IsAny<EditMessageCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }

        #endregion
    }
}
