using Core.Domain.Dtos.Languages;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Languages
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
