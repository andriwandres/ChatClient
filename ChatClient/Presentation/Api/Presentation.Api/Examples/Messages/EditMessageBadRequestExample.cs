using Core.Domain.Dtos.Messages;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Messages;

public class EditMessageBadRequestExample : IExamplesProvider<ValidationErrorViewModel>
{
    public ValidationErrorViewModel GetExamples()
    {
        const string htmlContentName = nameof(EditMessageBody.HtmlContent);

        return new ValidationErrorViewModel
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