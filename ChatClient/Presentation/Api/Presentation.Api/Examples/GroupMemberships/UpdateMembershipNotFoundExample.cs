using Core.Domain.ViewModels.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships;

public class UpdateMembershipNotFoundExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Membership with ID 'xxx' does not exist"
        };
    }
}