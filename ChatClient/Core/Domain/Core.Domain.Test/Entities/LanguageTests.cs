using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class LanguageTests
    {
        [Fact]
        public void LanguageConstructor_ShouldInitializeTranslationsNavigationProperty_WithEmptyCollection()
        {
            // Act
            Language language = new Language();

            // Assert
            Assert.Empty(language.Translations);
        }
    }
}
