using Core.Domain.ViewModels.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class DeleteMessageForbiddenExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = "Only the author of a message is allowed to delete a message"
        };
    }
}