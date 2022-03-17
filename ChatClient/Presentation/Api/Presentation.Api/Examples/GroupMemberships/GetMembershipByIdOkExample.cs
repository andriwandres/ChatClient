using Core.Domain.ViewModels.GroupMemberships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships;

public class GetMembershipByIdOkExample : IExamplesProvider<GroupMembershipViewModel>
{
    public GroupMembershipViewModel GetExamples()
    {
        return new GroupMembershipViewModel
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 1,
            UserName = "alfred_miller",
            IsAdmin = true
        };
    }
}