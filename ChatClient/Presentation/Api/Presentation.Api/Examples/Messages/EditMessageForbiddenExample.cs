using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class EditMessageForbiddenExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = "Only the author of a message is allowed to update a message"
        };
    }
}