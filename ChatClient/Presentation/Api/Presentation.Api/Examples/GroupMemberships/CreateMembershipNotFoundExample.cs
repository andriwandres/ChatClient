using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships;

public class CreateMembershipNotFoundExample : IMultipleExamplesProvider<ErrorViewModel>
{
    public IEnumerable<SwaggerExample<ErrorViewModel>> GetExamples()
    {
        return new[]
        {
            new SwaggerExample<ErrorViewModel>
            {
                Name = $"UserNotFound",
                Summary = $"User not found",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"User with ID 'xxx' does not exist"
                }
            },
            new SwaggerExample<ErrorViewModel>
            {
                Name = $"GroupNotFound",
                Summary = $"Group not found",
                Value = new ErrorViewModel
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Group with ID 'xxx' does not exist"
                }
            },
        };
    }
}