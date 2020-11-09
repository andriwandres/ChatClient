using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class TranslationRepositoryTests
    {
        private readonly IEnumerable<Translation> _translations = new[]
        {
            new Translation { TranslationId = 1, LanguageId = 1, Key = "Page.Group.LabelOne", Value = "Label 1" },
            new Translation { TranslationId = 2, LanguageId = 1, Key = "Page.Group.LabelTwo", Value = "Label 1" },
            new Translation { TranslationId = 3, LanguageId = 2, Key = "Page.Group.LabelOne", Value = "Text 1" },
            new Translation { TranslationId = 4, LanguageId = 2, Key = "Page.Group.LabelTwo", Value = "Text 1" },
        };

        [Fact]
        public async Task GetByLanguage_ShouldReturnAllTranslations_InEnglish()
        {
            // Arrange
            const int languageId = 1;

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository
                .GetByLanguage(languageId)
                .ToListAsync();

            // Assert
            Assert.NotNull(actualTranslations);
            Assert.NotEmpty(actualTranslations);

            Assert.Equal(2, actualTranslations.Count());
            Assert.All(actualTranslations, translation => Assert.Equal(1, translation.LanguageId));
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnEmptyList_WhenLanguageIdDoesNotExist()
        {
            // Arrange
            const int languageId = 1817;

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await  translationRepository
                .GetByLanguage(languageId)
                .ToListAsync();

            // Assert
            Assert.Empty(actualTranslations);
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnFilteredLanguages_WhenPatternIsProvided()
        {
            // Arrange
            const int languageId = 1;
            const string pattern = "Page.Group.LabelTwo";

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository
                .GetByLanguage(languageId, pattern)
                .ToListAsync();

            // Assert
            Assert.NotEmpty(actualTranslations);
            Assert.Single(actualTranslations);
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnFilteredLanguages_WhenPatternContainsWildcardAtBeginning()
        {
            // Arrange
            const int languageId = 1;
            const string pattern = "%LabelTwo";

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository
                .GetByLanguage(languageId, pattern)
                .ToListAsync();

            // Assert
            Assert.NotEmpty(actualTranslations);
            Assert.Single(actualTranslations);
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnFilteredLanguages_WhenPatternContainsWildcardAtTheEnd()
        {
            // Arrange
            const int languageId = 1;
            const string pattern = "Page.Group.Label%";

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository
                .GetByLanguage(languageId, pattern)
                .ToListAsync();

            // Assert
            Assert.NotEmpty(actualTranslations);
            Assert.Equal(2, actualTranslations.Count());
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnFilteredLanguages_WhenPatternContainsWildcardInBetween()
        {
            // Arrange
            const int languageId = 1;
            const string pattern = "Page.%.LabelTwo";

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository
                .GetByLanguage(languageId, pattern)
                .ToListAsync();

            // Assert
            Assert.NotEmpty(actualTranslations);
            Assert.Single(actualTranslations);
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnEmptyList_WhenPatternDoesNotMatch()
        {
            // Arrange
            const int languageId = 1;
            const string pattern = "unmatchable_pattern";

            Mock<DbSet<Translation>> translationDbSetMock = _translations
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Translations)
                .Returns(translationDbSetMock.Object);

            ITranslationRepository translationRepository = new TranslationRepository(contextMock.Object);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository
                .GetByLanguage(languageId, pattern)
                .ToListAsync();

            // Assert
            Assert.Empty(actualTranslations);
        }
    }
}
