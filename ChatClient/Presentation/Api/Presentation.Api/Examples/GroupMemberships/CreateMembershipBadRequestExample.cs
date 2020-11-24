using Core.Domain.Dtos.GroupMemberships;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class CreateMembershipBadRequestExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            const string userIdName = nameof(CreateMembershipBody.UserId);
            const string groupIdName = nameof(CreateMembershipBody.GroupId);

            return new[]
            {
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "GroupIdEmpty",
                    Summary = "Group ID is left empty",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                groupIdName,
                                new []
                                {
                                    $"'{groupIdName}' must not be empty"
                                }
                            }
                        }
                    },
                },
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "UserIdEmpty",
                    Summary = "User ID is left empty",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                userIdName,
                                new []
                                {
                                    $"'{userIdName}' must not be empty"
                                }
                            }
                        }
                    },
                },
            };
        }
    }
}
