using Core.Domain.Resources.Friendships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Friendships
{
    public class RequestFriendshipCreatedExample : IExamplesProvider<FriendshipResource>
    {
        public FriendshipResource GetExamples()
        {
            return new FriendshipResource
            {
                FriendshipId = 1,
                RequesterId = 1,
                AddresseeId = 2,
            };
        }
    }
}
