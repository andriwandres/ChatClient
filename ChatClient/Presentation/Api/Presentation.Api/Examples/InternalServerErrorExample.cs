using Core.Domain.Resources.Errors;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples;

public class InternalServerErrorExample : IExamplesProvider<ErrorResource>
{
    public ErrorResource GetExamples()
    {
        return new ErrorResource
        {
            StatusCode = 500,
            Message = "An unexpected error has occurred on the server"
        };
    }
}