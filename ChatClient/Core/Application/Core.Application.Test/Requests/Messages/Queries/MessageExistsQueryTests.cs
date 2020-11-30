using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class MessageExistsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public MessageExistsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task MessageExistsQueryHandler_ShouldReturnTrue_WhenMessageExists()
        {
            // Arrange
            MessageExistsQuery request = new MessageExistsQuery { MessageId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Messages.Exists(request.MessageId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            MessageExistsQuery.Handler handler = new MessageExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task MessageExistsQueryHandler_ShouldReturnFalse_WhenMessageDoesNotExist()
        {
            // Arrange
            MessageExistsQuery request = new MessageExistsQuery { MessageId = 442 };

            _unitOfWorkMock
                .Setup(m => m.Messages.Exists(request.MessageId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            MessageExistsQuery.Handler handler = new MessageExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
