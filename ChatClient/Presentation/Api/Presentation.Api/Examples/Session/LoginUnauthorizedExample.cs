using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Session;

public class LoginUnauthorizedExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            Message = "UserName, e-mail and/or password are incorrect"
        };
    }
}