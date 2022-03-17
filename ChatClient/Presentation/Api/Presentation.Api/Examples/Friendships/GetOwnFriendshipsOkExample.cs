using Core.Domain.Resources.Friendships;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Friendships;

public class GetOwnFriendshipsOkExample : IExamplesProvider<IEnumerable<FriendshipViewModel>>
{
    public IEnumerable<FriendshipViewModel> GetExamples()
    {
        return new[]
        {
            new FriendshipViewModel
            {
                FriendshipId = 1,
                RequesterId = 1,
                AddresseeId = 2
            },
            new FriendshipViewModel
            {
                FriendshipId = 2,
                RequesterId = 4,
                AddresseeId = 1
            },
        };
    }
}