using Core.Domain.ViewModels.Friendships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Friendships;

public class GetFriendshipByIdOkExample : IExamplesProvider<FriendshipViewModel>
{
    public FriendshipViewModel GetExamples()
    {
        return new FriendshipViewModel
        {
            FriendshipId = 1,
            RequesterId = 1,
            AddresseeId = 2,
        };
    }
}