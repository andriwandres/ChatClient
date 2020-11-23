using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Friendships
{
    public class GetFriendshipByIdNotFoundExample : IExamplesProvider<ErrorResource>
    {
        public ErrorResource GetExamples()
        {
            return new ErrorResource
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Friendship with ID 'xxx' does not exist"
            };
        }
    }
}
