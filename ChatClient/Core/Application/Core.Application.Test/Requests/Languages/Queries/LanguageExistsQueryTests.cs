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
        [Fact]
        public async Task LanguageExistsQueryHandler_ShouldReturnTrue_WhenLanguageExists()
        {
            // Arrange
            LanguageExistsQuery request = new LanguageExistsQuery {LanguageId = 1};

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Languages.Exists(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            LanguageExistsQuery.LanguageExistsQueryHandler handler =
                new LanguageExistsQuery.LanguageExistsQueryHandler(unitOfWorkMock.Object);

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

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Languages.Exists(1213, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            LanguageExistsQuery.LanguageExistsQueryHandler handler =
                new LanguageExistsQuery.LanguageExistsQueryHandler(unitOfWorkMock.Object);

            // Act
            bool exists = await handler.Handle(request);

            // Assert
            Assert.False(exists);
        }
    }
}
