using Core.Domain.Enums;
using Core.Domain.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Users;

public class GetUserProfileOkExample : IExamplesProvider<UserProfileViewModel>
{
    public UserProfileViewModel GetExamples()
    {
        return new UserProfileViewModel
        {
            UserId = 1,
            UserName = "alfred_miller",
            AvailabilityStatus = AvailabilityStatus.Online
        };
    }
}