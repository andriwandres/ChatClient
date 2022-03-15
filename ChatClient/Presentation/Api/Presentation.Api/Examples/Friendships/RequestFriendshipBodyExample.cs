using Core.Domain.Dtos.Friendships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Friendships;

public class RequestFriendshipBodyExample : IExamplesProvider<RequestFriendshipBody>
{
    public RequestFriendshipBody GetExamples()
    {
        return new RequestFriendshipBody
        {
            AddresseeId = 2
        };
    }
}