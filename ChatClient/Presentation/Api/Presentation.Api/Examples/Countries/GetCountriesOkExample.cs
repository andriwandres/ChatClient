using Core.Domain.Resources;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Countries
{
    public class GetCountriesOkExample : IExamplesProvider<IEnumerable<CountryResource>>
    {
        public IEnumerable<CountryResource> GetExamples()
        {
            return new[]
            {
                new CountryResource
                {
                    CountryId = 1,
                    Code = "CH",
                    Name = "Countries.Switzerland"
                },
                new CountryResource
                {
                    CountryId = 2,
                    Code = "DE",
                    Name = "Countries.Germany"
                },
                new CountryResource
                {
                    CountryId = 3,
                    Code = "FR",
                    Name = "Countries.France"
                },
            };
        }
    }
}
