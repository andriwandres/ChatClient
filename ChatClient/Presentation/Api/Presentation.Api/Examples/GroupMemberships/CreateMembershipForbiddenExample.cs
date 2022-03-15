using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships;

public class CreateMembershipForbiddenExample : IMultipleExamplesProvider<ErrorResource>
{
    public IEnumerable<SwaggerExample<ErrorResource>> GetExamples()
    {
        return new[]
        {
            new SwaggerExample<ErrorResource>
            {
                Name = "AlreadyMember",
                Summary = "User is already a member of this group",
                Value = new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "This user is already a member of this group"
                }
            },
            new SwaggerExample<ErrorResource>
            {
                Name = "NotPermitted",
                Summary = "Not permitted to create memberships",
                Value = new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You are not permitted to add users to this group. This privilege is only granted to administrators of the group"
                }
            },
        };
    }
}