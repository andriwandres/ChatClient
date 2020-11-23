using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class CreateMembershipNotFoundExample : IMultipleExamplesProvider<ErrorResource>
    {
        public IEnumerable<SwaggerExample<ErrorResource>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<ErrorResource>
                {
                    Name = $"UserNotFound",
                    Summary = $"User not found",
                    Value = new ErrorResource
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"User with ID 'xxx' does not exist"
                    }
                },
                new SwaggerExample<ErrorResource>
                {
                    Name = $"GroupNotFound",
                    Summary = $"Group not found",
                    Value = new ErrorResource
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Group with ID 'xxx' does not exist"
                    }
                },
            };
        }
    }
}
