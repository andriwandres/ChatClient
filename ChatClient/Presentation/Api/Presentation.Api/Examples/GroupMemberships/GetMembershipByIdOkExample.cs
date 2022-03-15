using Core.Domain.Resources.GroupMemberships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships;

public class GetMembershipByIdOkExample : IExamplesProvider<GroupMembershipResource>
{
    public GroupMembershipResource GetExamples()
    {
        return new GroupMembershipResource
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 1,
            UserName = "alfred_miller",
            IsAdmin = true
        };
    }
}