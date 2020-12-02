using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries
{
    public class RecipientExistsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RecipientExistsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task RecipientExistsQueryHandler_ShouldReturnTrue_WhenRecipientExists()
        {
            // Arrange
            RecipientExistsQuery request = new RecipientExistsQuery {RecipientId = 1};

            _unitOfWorkMock
                .Setup(m => m.Recipients.Exists(request.RecipientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            RecipientExistsQuery.Handler handler = new RecipientExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task RecipientExistsQueryHandler_ShouldReturnFalse_WhenRecipientDoesNotExist()
        {
            // Arrange
            RecipientExistsQuery request = new RecipientExistsQuery { RecipientId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Recipients.Exists(request.RecipientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            RecipientExistsQuery.Handler handler = new RecipientExistsQuery.Handler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
