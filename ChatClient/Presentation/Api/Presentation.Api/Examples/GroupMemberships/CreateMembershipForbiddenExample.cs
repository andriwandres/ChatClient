using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class CreateMembershipForbiddenExample : IExamplesProvider<ErrorResource>
    {
        public ErrorResource GetExamples()
        {
            return new ErrorResource
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Message = "This user is already a member of this group"
            };
        }
    }
}
