using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Translations
{
    public class GetTranslationsByLanguageResponseExample : IExamplesProvider<IDictionary<string, string>>
    {
        public IDictionary<string, string> GetExamples()
        {
            return new Dictionary<string, string>
            {
                { "Home.Header.ApplicationTitle", "MyApplication" },
                { "Home.Header.HamburgerMenu.Opened.Tooltip", "Close menu" },
                { "Home.Header.HamburgerMenu.Closed.Tooltip", "Open menu" },
            };
        }
    }
}
