using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Users
{
    public class GetUserProfileOkExample : IExamplesProvider<UserProfileResource>
    {
        public UserProfileResource GetExamples()
        {
            return new UserProfileResource
            {
                UserId = 1,
                UserName = "alfred_miller",
                AvailabilityStatusId = AvailabilityStatusId.Online
            };
        }
    }
}
