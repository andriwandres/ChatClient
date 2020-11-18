using Presentation.Api.Examples.Languages;
using Presentation.Api.Examples.Translations;
using Xunit;

namespace Presentation.Api.Test.Examples
{
    public class LanguagesExampleTests
    {
        [Fact]
        public void GetAllLanguagesResponseExample_ReturnsValidExample()
        {
            GetAllLanguagesResponseExample provider = new GetAllLanguagesResponseExample();

            Assert.NotNull(provider.GetExamples());
            Assert.NotEmpty(provider.GetExamples());
        }

        [Fact]
        public void GetTranslationsByLanguageResponseExample_ReturnsValidExample()
        {
            GetTranslationsByLanguageResponseExample provider = new GetTranslationsByLanguageResponseExample();

            Assert.NotNull(provider.GetExamples());
            Assert.NotEmpty(provider.GetExamples());
        }
    }
}
