using Core.Domain.ViewModels.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Friendships;

public class GetFriendshipByIdNotFoundExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Friendship with ID 'xxx' does not exist"
        };
    }
}