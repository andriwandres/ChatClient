using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Session
{
    public class LoginCredentialsInvalidResponseExample : IExamplesProvider<ErrorResource>
    {
        public ErrorResource GetExamples()
        {
            return new ErrorResource
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "UserName, e-mail and/or password are incorrect"
            };
        }
    }
}
