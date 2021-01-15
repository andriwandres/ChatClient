using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Dtos.Messages;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages
{
    public class GetMessagesWithRecipientBadRequestExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        // TODO: Write examples according to validation rules, once figured out
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            const string limitName = nameof(GetMessagesWithRecipientQueryParams.Limit);

            return new[]
            {
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "LimitEmpty",
                    Summary = "Limit is not specified",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                limitName,
                                new [] { "The limit must not be empty" }
                            }
                        }
                    }
                },
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
            };
        }
    }
}
