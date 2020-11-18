using Core.Domain.Dtos.Friendships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Friendships
{
    public class RequestFriendshipRequestExample : IExamplesProvider<RequestFriendshipDto>
    {
        public RequestFriendshipDto GetExamples()
        {
            return new RequestFriendshipDto
            {
                AddresseeId = 2
            };
        }
    }
}
