using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages
{
    public class SendMessageForbiddenExample : IMultipleExamplesProvider<ErrorResource>
    {
        public IEnumerable<SwaggerExample<ErrorResource>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<ErrorResource>
                {
                    Name = "MessageHimself",
                    Summary = "User tried messaging himself",
                    Value = new ErrorResource
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Message = "You cannot write messages to yourself"
                    }
                },
                new SwaggerExample<ErrorResource>
                {
                    Name = "ForeignMessage",
                    Summary = "User tried answering message from a foreign chat",
                    Value = new ErrorResource
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Message = "You cannot answer messages from a foreign chat"
                    }
                },
            };
        }
    }
}
