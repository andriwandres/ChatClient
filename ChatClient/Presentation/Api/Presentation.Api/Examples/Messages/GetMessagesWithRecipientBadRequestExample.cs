using Core.Domain.Dtos.Messages;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages;

public class GetMessagesWithRecipientBadRequestExample : IMultipleExamplesProvider<ValidationErrorResource>
{
    public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
    {
        const string limitName = nameof(GetMessagesWithRecipientQueryParams.Limit);

        return new[]
        {
            new SwaggerExample<ValidationErrorResource>
            {
                Name = "LimitBelowZero",
                Summary = "Limit is below zero",
                Value = new ValidationErrorResource
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
            new SwaggerExample<ValidationErrorResource>
            {
                Name = "BeforeAfterOverlap",
                Summary = "'Before' is before 'After'",
                Value = new ValidationErrorResource
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