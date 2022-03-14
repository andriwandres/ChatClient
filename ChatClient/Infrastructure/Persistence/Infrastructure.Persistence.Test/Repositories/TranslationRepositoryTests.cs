using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class TranslationRepositoryTests
    {
        private readonly IChatContext _context;
        private readonly IEnumerable<Translation> _translations = new[]
        {
            new Translation { TranslationId = 1, LanguageId = 1, Key = "Page.Group.LabelOne", Value = "Label 1" },
            new Translation { TranslationId = 2, LanguageId = 1, Key = "Page.Group.LabelTwo", Value = "Label 1" },
            new Translation { TranslationId = 3, LanguageId = 2, Key = "Page.Group.LabelOne", Value = "Text 1" },
            new Translation { TranslationId = 4, LanguageId = 2, Key = "Page.Group.LabelTwo", Value = "Text 1" },
        };

        public TranslationRepositoryTests()
        {
            _context = TestContextFactory.Create();
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnAllTranslations_InEnglish()
        {
            // Arrange
            const int languageId = 1;

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId);

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

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId);

            // Assert
            Assert.Empty(actualTranslations);
        }

        [Fact]
        public async Task GetByLanguage_ShouldReturnFilteredLanguages_WhenPatternIsProvided()
        {
            // Arrange
            const int languageId = 1;
            const string pattern = "Page.Group.LabelTwo";

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId, pattern);

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

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId, pattern);

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

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId, pattern);

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

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId, pattern);

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

            await _context.Translations.AddRangeAsync(_translations);
            await _context.SaveChangesAsync();

            ITranslationRepository translationRepository = new TranslationRepository(_context);

            // Act
            IEnumerable<Translation> actualTranslations = await translationRepository.GetByLanguage(languageId, pattern);

            // Assert
            Assert.Empty(actualTranslations);
        }
    }
}
