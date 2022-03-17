using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class GetMessageByIdNotFoundExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Message with ID 'xxx' does not exist"
        };
    }
}