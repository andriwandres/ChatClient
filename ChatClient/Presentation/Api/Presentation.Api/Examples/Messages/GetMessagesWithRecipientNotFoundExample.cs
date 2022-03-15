using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class GetMessagesWithRecipientNotFoundExample : IExamplesProvider<ErrorResource>
{
    public ErrorResource GetExamples()
    {
        return new ErrorResource
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Recipient with ID 'xxx' does not exist"
        };
    }
}