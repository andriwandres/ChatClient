using Core.Domain.Resources.Friendships;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Friendships;

public class GetOwnFriendshipsOkExample : IExamplesProvider<IEnumerable<FriendshipResource>>
{
    public IEnumerable<FriendshipResource> GetExamples()
    {
        return new[]
        {
            new FriendshipResource
            {
                FriendshipId = 1,
                RequesterId = 1,
                AddresseeId = 2
            },
            new FriendshipResource
            {
                FriendshipId = 2,
                RequesterId = 4,
                AddresseeId = 1
            },
        };
    }
}