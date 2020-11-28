using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class DeleteMembershipForbiddenExample : IExamplesProvider<ErrorResource>
    {
        public ErrorResource GetExamples()
        {
            return new ErrorResource
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Message = "You are not permitted to delete users from this group. This privilege is only granted to administrators of the group"
            };
        }
    }
}
