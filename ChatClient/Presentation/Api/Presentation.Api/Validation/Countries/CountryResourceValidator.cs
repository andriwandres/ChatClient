using Core.Domain.Resources;
using FluentValidation;

namespace Presentation.Api.Validation.Countries
{
    public class CountryResourceValidator : AbstractValidator<CountryResource>
    {
        public CountryResourceValidator()
        {
            RuleFor(country => country.CountryId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(country => country.Code)
                .NotEmpty()
                .Length(2);

            RuleFor(country => country.Name)
                .NotEmpty();
        }
    }
}
