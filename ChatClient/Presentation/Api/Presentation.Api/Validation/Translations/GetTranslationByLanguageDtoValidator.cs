using System.Text.RegularExpressions;
using Core.Domain.Dtos.Languages;
using FluentValidation;

namespace Presentation.Api.Validation.Translations
{
    public class GetTranslationByLanguageDtoValidator : AbstractValidator<GetTranslationsByLanguageDto>
    {
        public GetTranslationByLanguageDtoValidator()
        {
            const string patternName = nameof(GetTranslationsByLanguageDto.Pattern);
            RuleFor(model => model.Pattern)
                .Matches(new Regex(@"^[A-Za-z0-9.*]+?$"))
                .WithMessage($"'{patternName}' contains illegal characters. It must only contain alphanumeric characters including punctuation (.) and wildcard characters (*)");
        }
    }
}
