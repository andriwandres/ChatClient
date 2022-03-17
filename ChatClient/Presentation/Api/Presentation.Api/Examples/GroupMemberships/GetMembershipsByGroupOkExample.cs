using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.GroupMemberships;

namespace Presentation.Api.Examples.GroupMemberships;

public class GetMembershipsByGroupOkExample : IExamplesProvider<IEnumerable<GroupMembershipViewModel>>
{
    public IEnumerable<GroupMembershipViewModel> GetExamples()
    {
        return new[]
        {
            new GroupMembershipViewModel
            {
                GroupMembershipId = 1,
                GroupId = 1,
                UserId = 1,
                UserName = "alfred_miller",
                IsAdmin = true
            },
            new GroupMembershipViewModel
            {
                GroupMembershipId = 2,
                GroupId = 1,
                UserId = 2,
                UserName = "joseph_bogard",
                IsAdmin = false
            },
        };
    }
}