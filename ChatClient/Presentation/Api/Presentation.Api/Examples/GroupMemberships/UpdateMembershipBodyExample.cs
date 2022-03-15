using Core.Domain.Dtos.GroupMemberships;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships;

public class UpdateMembershipBodyExample : IExamplesProvider<UpdateMembershipBody>
{
    public UpdateMembershipBody GetExamples()
    {
        return new UpdateMembershipBody
        {
            IsAdmin = true
        };
    }
}