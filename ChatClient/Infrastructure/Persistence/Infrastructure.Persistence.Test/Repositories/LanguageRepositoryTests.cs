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
    public class LanguageRepositoryTests
    {
        private readonly IChatContext _context;

        public LanguageRepositoryTests()
        {
            _context = TestContextFactory.Create();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllLanguages()
        {
            // Arrange
            IEnumerable<Language> languages = new []
            {
                new Language { LanguageId = 1, Code = "EN", Name = "Language.English" },
                new Language { LanguageId = 2, Code = "DE", Name = "Language.German" },
                new Language { LanguageId = 3, Code = "FR", Name = "Language.French" },
            };

            await _context.Languages.AddRangeAsync(languages);
            await _context.SaveChangesAsync();

            ILanguageRepository languageRepository = new LanguageRepository(_context);

            // Act
            IEnumerable<Language> actualLanguages = await languageRepository.GetAllAsync();

            // Assert
            Assert.NotEmpty(actualLanguages);
            Assert.Equal(3, actualLanguages.Count());
        }

        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenLanguageExists()
        {
            // Arrange
            const int languageId = 1;

            IEnumerable<Language> languages = new[]
            {
                new Language { LanguageId = 1, Code = "EN", Name = "Language.English" },
                new Language { LanguageId = 2, Code = "DE", Name = "Language.German" },
                new Language { LanguageId = 3, Code = "FR", Name = "Language.French" },
            };

            await _context.Languages.AddRangeAsync(languages);
            await _context.SaveChangesAsync();

            ILanguageRepository languageRepository = new LanguageRepository(_context);

            // Act
            bool exists = await languageRepository.Exists(languageId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Exists_ShouldReturnFalse_WhenLanguageDoesNotExist()
        {
            // Arrange
            const int languageId = 51;

            IEnumerable<Language> languages = new[]
            {
                new Language { LanguageId = 1, Code = "EN", Name = "Language.English" },
                new Language { LanguageId = 2, Code = "DE", Name = "Language.German" },
                new Language { LanguageId = 3, Code = "FR", Name = "Language.French" },
            };

            await _context.Languages.AddRangeAsync(languages);
            await _context.SaveChangesAsync();

            ILanguageRepository languageRepository = new LanguageRepository(_context);

            // Act
            bool exists = await languageRepository.Exists(languageId);

            // Assert
            Assert.False(exists);
        }
    }
}
