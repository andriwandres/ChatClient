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
    public class DeleteMessageQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteMessageQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task DeleteMessageCommandHandler_ShouldSoftDeleteMessage()
        {
            // Arrange
            DeleteMessageCommand request = new DeleteMessageCommand { MessageId = 1 };

            IQueryable<Message> databaseMessage = new[]
            {
                new Message { MessageId = 1, IsDeleted = false }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            Message passedMessage = null;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(databaseMessage);

            _unitOfWorkMock
                .Setup(m => m.Messages.Update(It.IsAny<Message>()))
                .Callback<Message>(m => passedMessage = m);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            DeleteMessageCommand.Handler handler = new DeleteMessageCommand.Handler(_unitOfWorkMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.Messages.Update(It.IsAny<Message>()), Times.Once);

            Assert.NotNull(passedMessage);
            Assert.Equal(request.MessageId, passedMessage.MessageId);
            Assert.True(passedMessage.IsDeleted);

            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
