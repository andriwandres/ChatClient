using System.Text.RegularExpressions;
using Core.Domain.Dtos.Languages;
using FluentValidation;

namespace Presentation.Api.Validation.Translations
{
    public class GetTranslationByLanguageDtoValidator : AbstractValidator<GetTranslationsByLanguageQueryParams>
    {
        public GetTranslationByLanguageDtoValidator()
        {
            const string patternName = nameof(GetTranslationsByLanguageQueryParams.Pattern);
            RuleFor(model => model.Pattern)
                .Matches(new Regex(@"^[A-Za-z0-9.*]+?$"))
                .WithMessage($"'{patternName}' contains illegal characters. It must only contain alphanumeric characters including punctuation (.) and wildcard characters (*)");
        }
    }
}
