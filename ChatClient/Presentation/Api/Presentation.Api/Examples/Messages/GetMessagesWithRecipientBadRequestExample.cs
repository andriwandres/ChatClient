using Core.Domain.Dtos.Messages;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Messages;

public class GetMessagesWithRecipientBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const string limitName = nameof(GetMessagesWithRecipientQueryParams.Limit);

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "LimitBelowZero",
                Summary = "Limit is below zero",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            limitName,
                            new [] { "Limit must be greater than zero" }
                        }
                    }
                }
            },
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "BeforeAfterOverlap",
                Summary = "'Before' is before 'After'",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            limitName,
                            new [] { "'Before' must be at a later point in time than 'After'" }
                        }
                    }
                }
            },
        };
    }
}