using Core.Domain.Dtos.Messages;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages
{
    public class SendMessageBadRequestExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            const string recipientIdName = nameof(SendMessageBody.RecipientId);
            const string htmlContentName = nameof(SendMessageBody.HtmlContent);

            return new[]
            {
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "RecipientIdEmpty",
                    Summary = $"{recipientIdName} is left empty",
                    Value = new ValidationErrorResource
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
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "HtmlContentEmpty",
                    Summary = $"{htmlContentName} is left empty",
                    Value = new ValidationErrorResource
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
}
