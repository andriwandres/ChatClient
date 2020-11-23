using Core.Domain.Dtos.Languages;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Translations
{
    public class GetTranslationsByLanguageBadRequestExample : IExamplesProvider<ValidationErrorResource>
    {
        public ValidationErrorResource GetExamples()
        {
            const string patternName = nameof(GetTranslationsByLanguageQueryParams.Pattern);

            return new ValidationErrorResource
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "One or multiple validation errors occurred",
                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        patternName, 
                        new[]
                        {
                            $"'{patternName}' contains illegal characters. It must only contain alphanumeric characters including punctuation (.) and wildcard characters (*)"
                        }
                    }
                }
            };
        }
    }
}