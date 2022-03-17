using Core.Domain.ViewModels;
using FluentValidation;

namespace Presentation.Api.Validation.Countries;

public class CountryResourceValidator : AbstractValidator<CountryViewModel>
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