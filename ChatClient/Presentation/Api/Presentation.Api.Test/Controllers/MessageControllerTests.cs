using Core.Application.Requests.Messages.Queries;
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
        private readonly Mock<IMediator> _mediatorMock;

        public MessageControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
        }

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
    }
}
