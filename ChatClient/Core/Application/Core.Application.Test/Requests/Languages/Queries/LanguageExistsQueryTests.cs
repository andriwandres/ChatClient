using Core.Application.Database;
using Core.Application.Requests.Languages.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Languages.Queries
{
    public class LanguageExistsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public LanguageExistsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task LanguageExistsQueryHandler_ShouldReturnTrue_WhenLanguageExists()
        {
            // Arrange
            LanguageExistsQuery request = new LanguageExistsQuery {LanguageId = 1};

            _unitOfWorkMock
                .Setup(m => m.Languages.Exists(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            LanguageExistsQuery.LanguageExistsQueryHandler handler =
                new LanguageExistsQuery.LanguageExistsQueryHandler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task LanguageExistsQueryHandler_ShouldReturnFalse_WhenLanguageDoesNotExists()
        {
            // Arrange
            LanguageExistsQuery request = new LanguageExistsQuery { LanguageId = 1213 };

            _unitOfWorkMock
                .Setup(m => m.Languages.Exists(request.LanguageId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            LanguageExistsQuery.LanguageExistsQueryHandler handler =
                new LanguageExistsQuery.LanguageExistsQueryHandler(_unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
