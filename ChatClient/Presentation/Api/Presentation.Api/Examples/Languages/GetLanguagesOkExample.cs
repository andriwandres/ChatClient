using Core.Domain.Resources.Languages;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Languages
{
    public class GetLanguagesOkExample : IExamplesProvider<IEnumerable<LanguageResource>>
    {
        public IEnumerable<LanguageResource> GetExamples()
        {
            return new[]
            {
                new LanguageResource
                {
                    LanguageId = 1,
                    Code = "EN",
                    Name = "Languages.English"
                },
                new LanguageResource
                {
                    LanguageId = 2,
                    Code = "DE",
                    Name = "Languages.German"
                },
            };
        }
    }
}
