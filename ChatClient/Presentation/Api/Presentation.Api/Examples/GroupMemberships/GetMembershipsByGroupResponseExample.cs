using Core.Domain.Resources.GroupMemberships;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class GetMembershipsByGroupResponseExample : IExamplesProvider<IEnumerable<GroupMembershipResource>>
    {
        public IEnumerable<GroupMembershipResource> GetExamples()
        {
            return new[]
            {
                new GroupMembershipResource
                {
                    GroupMembershipId = 1,
                    GroupId = 1,
                    UserId = 1,
                    UserName = "alfred_miller",
                    IsAdmin = true
                },
                new GroupMembershipResource
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
}
