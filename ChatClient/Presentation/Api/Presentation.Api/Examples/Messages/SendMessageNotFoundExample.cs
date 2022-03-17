using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages;

public class SendMessageNotFoundExample : IMultipleExamplesProvider<ErrorViewModel>
{
    public IEnumerable<SwaggerExample<ErrorViewModel>> GetExamples()
    {
        return new[]
        {
            new SwaggerExample<ErrorViewModel>
            {
                Name = "RecipientNotFound",
                Summary = "Recipient does not exist",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Recipient with ID 'xxx' does not exist"
                }
            },
            new SwaggerExample<ErrorViewModel>
            {
                Name = "ParentMessageNotFound",
                Summary = "Parent message does not exist",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Parent message with ID 'xxx' does not exist"
                }
            },
        };
    }
}