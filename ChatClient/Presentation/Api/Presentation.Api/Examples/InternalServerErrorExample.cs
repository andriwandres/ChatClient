using Core.Domain.ViewModels.Errors;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples;

public class InternalServerErrorExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = 500,
            Message = "An unexpected error has occurred on the server"
        };
    }
}