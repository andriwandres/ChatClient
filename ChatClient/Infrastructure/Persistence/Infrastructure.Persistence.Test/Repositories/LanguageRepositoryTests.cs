using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class LanguageRepositoryTests
    {
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

            Mock<DbSet<Language>> languageDbSetMock = languages
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Languages)
                .Returns(languageDbSetMock.Object);

            ILanguageRepository languageRepository = new LanguageRepository(contextMock.Object);

            // Act
            IEnumerable<Language> actualLanguages = await languageRepository
                .GetAll()
                .ToListAsync();

            // Assert
            Assert.NotEmpty(actualLanguages);
            Assert.Equal(3, actualLanguages.Count());
        }
    }
}
