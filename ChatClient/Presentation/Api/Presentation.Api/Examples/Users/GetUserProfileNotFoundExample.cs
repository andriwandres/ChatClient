using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Users;

public class GetUserProfileNotFoundExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = "A user with the ID 'xxx' does not exist"
        };
    }
}