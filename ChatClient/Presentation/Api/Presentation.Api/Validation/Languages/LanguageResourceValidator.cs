using Core.Domain.Resources.Languages;
using FluentValidation;

namespace Presentation.Api.Validation.Languages
{
    public class LanguageResourceValidator : AbstractValidator<LanguageResource>
    {
        public LanguageResourceValidator()
        {
            RuleFor(model => model.LanguageId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(model => model.Code)
                .NotEmpty()
                .Length(2);

            RuleFor(model => model.Name)
                .NotEmpty();
        }
    }
}
