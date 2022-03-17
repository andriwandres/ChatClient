using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Messages;

public class SendMessageForbiddenExample : IMultipleExamplesProvider<ErrorViewModel>
{
    public IEnumerable<SwaggerExample<ErrorViewModel>> GetExamples()
    {
        return new[]
        {
            new SwaggerExample<ErrorViewModel>
            {
                Name = "MessageHimself",
                Summary = "User tried messaging himself",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You cannot write messages to yourself"
                }
            },
            new SwaggerExample<ErrorViewModel>
            {
                Name = "ForeignMessage",
                Summary = "User tried answering message from a foreign chat",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You cannot answer messages from a foreign chat"
                }
            },
        };
    }
}