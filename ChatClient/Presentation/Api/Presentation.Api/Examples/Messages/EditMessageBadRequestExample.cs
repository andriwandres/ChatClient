using Core.Domain.Dtos.Messages;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages;

public class EditMessageBadRequestExample : IExamplesProvider<ValidationErrorResource>
{
    public ValidationErrorResource GetExamples()
    {
        const string htmlContentName = nameof(EditMessageBody.HtmlContent);

        return new ValidationErrorResource
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "One or multiple validation errors occurred",
            Errors = new Dictionary<string, IEnumerable<string>>
            {
                {
                    htmlContentName,
                    new[]
                    {
                        $"'{htmlContentName}' must not be enmpty"
                    }
                }
            }
        };
    }
}