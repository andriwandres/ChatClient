using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Friendships;

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