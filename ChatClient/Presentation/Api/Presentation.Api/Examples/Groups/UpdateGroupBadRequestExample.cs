using Core.Domain.Dtos.Groups;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Groups;

public class UpdateGroupBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const int nameMinLength = 2;
        const string nameName = nameof(UpdateGroupBody.Name);

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "NameEmpty",
                Summary = "Name is left empty",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            nameName,
                            new []
                            {
                                $"'{nameName}' must not be empty"
                            }
                        }
                    }
                }
            },
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "NameTooShort",
                Summary = "Name is too short",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            nameName,
                            new []
                            {
                                $"'{nameName}' must be at least {nameMinLength} characters long. You entered xxx characters"
                            }
                        }
                    }
                }
            },
        };
    }
}