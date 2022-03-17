using Core.Domain.Dtos.GroupMemberships;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.GroupMemberships;

public class CreateMembershipBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const string userIdName = nameof(CreateMembershipBody.UserId);
        const string groupIdName = nameof(CreateMembershipBody.GroupId);

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "GroupIdEmpty",
                Summary = "Group ID is left empty",
                Value = new ValidationErrorViewModel
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
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "UserIdEmpty",
                Summary = "User ID is left empty",
                Value = new ValidationErrorViewModel
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