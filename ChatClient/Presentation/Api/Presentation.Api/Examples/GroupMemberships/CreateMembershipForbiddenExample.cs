using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.GroupMemberships;

public class CreateMembershipForbiddenExample : IMultipleExamplesProvider<ErrorViewModel>
{
    public IEnumerable<SwaggerExample<ErrorViewModel>> GetExamples()
    {
        return new[]
        {
            new SwaggerExample<ErrorViewModel>
            {
                Name = "AlreadyMember",
                Summary = "User is already a member of this group",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "This user is already a member of this group"
                }
            },
            new SwaggerExample<ErrorViewModel>
            {
                Name = "NotPermitted",
                Summary = "Not permitted to create memberships",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You are not permitted to add users to this group. This privilege is only granted to administrators of the group"
                }
            },
        };
    }
}