using Core.Domain.Dtos.Languages;
using FluentValidation;

namespace Presentation.Api.Validation.Languages
{
    public class GetTranslationByLanguageDtoValidator : AbstractValidator<GetTranslationsByLanguageDto>
    {
        public GetTranslationByLanguageDtoValidator()
        {
            RuleFor(model => model.Pattern);
        }
    }
}
