using Core.Application.Database;
using Core.Application.Requests.Messages.Commands;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Commands
{
    public class EditMessageCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public EditMessageCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task EditMessageCommandHandler_ShouldUpdateMessagesContent()
        {
            // Arrange
            EditMessageCommand request = new EditMessageCommand
            {
                MessageId = 1,
                HtmlContent = "<p>hello world</p>"
            };

            IQueryable<Message> databaseMessage = new[]
            {
                new Message { MessageId = 1, HtmlContent = "<p>original</p>", IsEdited = false }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(databaseMessage);

            Message passedMessage = null;

            _unitOfWorkMock
                .Setup(m => m.Messages.Update(It.IsAny<Message>()))
                .Callback<Message>(m => passedMessage = m);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            EditMessageCommand.Handler handler = new EditMessageCommand.Handler(_unitOfWorkMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.Messages.Update(It.IsAny<Message>()), Times.Once);
            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            Assert.NotNull(passedMessage);
            Assert.Equal(request.HtmlContent, passedMessage.HtmlContent);
            Assert.True(passedMessage.IsEdited);
        }
    }
}
