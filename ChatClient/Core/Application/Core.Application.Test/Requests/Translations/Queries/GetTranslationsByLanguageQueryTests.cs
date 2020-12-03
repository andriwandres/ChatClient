using Core.Application.Database;
using Core.Application.Requests.Translations.Queries;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Translations.Queries
{
    public class GetTranslationsByLanguageQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetTranslationsByLanguageQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task GetTranslationsByLanguageQueryHandler_ShouldReturnEmptyDictionary_WhenLanguageDoesNotExist()
        {
            // Arrange
            GetTranslationsByLanguageQuery request = new GetTranslationsByLanguageQuery { LanguageId = 8917 };

            Mock<IQueryable<Translation>> translationQueryableMock = Enumerable
                .Empty<Translation>()
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Translations.GetByLanguage(It.IsAny<int>()))
                .Returns(translationQueryableMock.Object);

            GetTranslationsByLanguageQuery.GetTranslationsByLanguageQueryHandler handler
                = new GetTranslationsByLanguageQuery.GetTranslationsByLanguageQueryHandler(_unitOfWorkMock.Object);

            // Act
            IDictionary<string, string> translations = await handler.Handle(request);

            // Assert
            Assert.NotNull(translations);
            Assert.Empty(translations);
        }

        [Fact]
        public async Task GetTranslationsByLanguageQueryHandler_ShouldReturnTranslationDictionary_WhenLanguageExists()
        {
            // Arrange
            GetTranslationsByLanguageQuery request = new GetTranslationsByLanguageQuery { LanguageId = 1 };

            IEnumerable<Translation> expectedTranslations = new []
            {
                new Translation { TranslationId = 1, Key = "key", Value = "value" }
            };

            Mock<IQueryable<Translation>> translationQueryableMock = expectedTranslations
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Translations.GetByLanguage(It.IsAny<int>()))
                .Returns(translationQueryableMock.Object);

            GetTranslationsByLanguageQuery.GetTranslationsByLanguageQueryHandler handler
                = new GetTranslationsByLanguageQuery.GetTranslationsByLanguageQueryHandler(_unitOfWorkMock.Object);

            // Act
            IDictionary<string, string> translations = await handler.Handle(request);

            // Assert
            Assert.NotNull(translations);
            Assert.NotEmpty(translations);
            Assert.Single(translations);

            Assert.Equal("key", translations.First().Key);
            Assert.Equal("value", translations.First().Value);
        }

        [Fact]
        public async Task GetTranslationsByLanguageQueryHandler_ShouldReturnTranslationDictionary_WhenLanguageExistsAndPatternMatches()
        {
            // Arrange
            GetTranslationsByLanguageQuery request = new GetTranslationsByLanguageQuery { LanguageId = 1, Pattern = "key" };

            IEnumerable<Translation> expectedTranslations = new[]
            {
                new Translation { TranslationId = 1, Key = "key", Value = "value" }
            };

            Mock<IQueryable<Translation>> translationQueryableMock = expectedTranslations
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Translations.GetByLanguage(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(translationQueryableMock.Object);

            GetTranslationsByLanguageQuery.GetTranslationsByLanguageQueryHandler handler
                = new GetTranslationsByLanguageQuery.GetTranslationsByLanguageQueryHandler(_unitOfWorkMock.Object);

            // Act
            IDictionary<string, string> translations = await handler.Handle(request);

            // Assert
            Assert.NotNull(translations);
            Assert.NotEmpty(translations);
            Assert.Single(translations);

            Assert.Equal("key", translations.First().Key);
            Assert.Equal("value", translations.First().Value);
        }
    }
}
