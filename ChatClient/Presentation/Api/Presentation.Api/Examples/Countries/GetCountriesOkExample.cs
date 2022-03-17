using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels;

namespace Presentation.Api.Examples.Countries;

public class GetCountriesOkExample : IExamplesProvider<IEnumerable<CountryViewModel>>
{
    public IEnumerable<CountryViewModel> GetExamples()
    {
        return new[]
        {
            new CountryViewModel
            {
                CountryId = 1,
                Code = "CH",
                Name = "Countries.Switzerland"
            },
            new CountryViewModel
            {
                CountryId = 2,
                Code = "DE",
                Name = "Countries.Germany"
            },
            new CountryViewModel
            {
                CountryId = 3,
                Code = "FR",
                Name = "Countries.France"
            },
        };
    }
}