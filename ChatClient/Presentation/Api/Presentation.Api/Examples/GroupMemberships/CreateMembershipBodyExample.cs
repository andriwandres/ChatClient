using Core.Domain.Dtos.GroupMemberships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class CreateMembershipBodyExample : IExamplesProvider<CreateMembershipDto>
    {
        public CreateMembershipDto GetExamples()
        {
            return new CreateMembershipDto
            {
                UserId = 1,
                GroupId = 1
            };
        }
    }
}
