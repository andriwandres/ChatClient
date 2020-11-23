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
            GetLanguagesOkExample provider = new GetLanguagesOkExample();

            Assert.NotNull(provider.GetExamples());
            Assert.NotEmpty(provider.GetExamples());
        }

        [Fact]
        public void GetTranslationsByLanguageResponseExample_ReturnsValidExample()
        {
            GetTranslationsByLanguageOkExample provider = new GetTranslationsByLanguageOkExample();

            Assert.NotNull(provider.GetExamples());
            Assert.NotEmpty(provider.GetExamples());
        }
    }
}
