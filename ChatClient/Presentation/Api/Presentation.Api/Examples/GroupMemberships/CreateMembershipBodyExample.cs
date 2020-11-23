using Core.Domain.Dtos.GroupMemberships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class CreateMembershipBodyExample : IExamplesProvider<CreateMembershipBody>
    {
        public CreateMembershipBody GetExamples()
        {
            return new CreateMembershipBody
            {
                UserId = 1,
                GroupId = 1
            };
        }
    }
}
