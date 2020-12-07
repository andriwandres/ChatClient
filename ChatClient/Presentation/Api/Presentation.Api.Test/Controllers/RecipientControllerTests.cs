using Core.Application.Requests.Messages.Queries;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class RecipientControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        public RecipientControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetMessagesWithRecipient_ShouldReturnNotFoundResult_WhenRecipientDoesNotExist()
        {
            // Arrange
            const int recipientId = 424;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            RecipientController controller = new RecipientController(_mediatorMock.Object);

            // Act
            ActionResult<IEnumerable<ChatMessageResource>> response = await controller.GetMessagesWithRecipient(recipientId);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.NotNull(error);
            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task GetMessagesWithRecipient_ShouldReturnRecipients_WhenRecipientExist()
        {
            // Arrange
            const int recipientId = 1;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RecipientExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetMessagesWithRecipientQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ChatMessageResource>());

            RecipientController controller = new RecipientController(_mediatorMock.Object);

            // Act
            ActionResult<IEnumerable<ChatMessageResource>> response = await controller.GetMessagesWithRecipient(recipientId);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}