using Core.Domain.Dtos.Messages;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Messages;

public class SendMessageBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const string recipientIdName = nameof(SendMessageBody.RecipientId);
        const string htmlContentName = nameof(SendMessageBody.HtmlContent);

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "RecipientIdEmpty",
                Summary = $"{recipientIdName} is left empty",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            recipientIdName,
                            new []
                            {
                                $"'{recipientIdName}' must not be empty"
                            }
                        }
                    }
                }
            },
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "HtmlContentEmpty",
                Summary = $"{htmlContentName} is left empty",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            htmlContentName,
                            new []
                            {
                                $"'{htmlContentName}' must not be empty"
                            }
                        }
                    }
                }
            }
        };
    }
}