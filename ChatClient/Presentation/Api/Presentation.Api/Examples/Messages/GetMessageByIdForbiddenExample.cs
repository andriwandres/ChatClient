using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class GetMessageByIdForbiddenExample : IExamplesProvider<ErrorResource>
{
    public ErrorResource GetExamples()
    {
        return new ErrorResource
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = "The user is not permitted to see this message. He may only see messages that he received or wrote himself"
        };
    }
}