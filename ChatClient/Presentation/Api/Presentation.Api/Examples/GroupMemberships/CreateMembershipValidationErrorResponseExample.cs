using Core.Domain.Dtos.GroupMemberships;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class CreateMembershipValidationErrorResponseExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            const string userIdName = nameof(CreateMembershipDto.UserId);
            const string groupIdName = nameof(CreateMembershipDto.GroupId);

            return new[]
            {
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = $"{userIdName}Empty",
                    Summary = $"{userIdName} is invalid",
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
                    }
                },
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = $"{groupIdName}Empty",
                    Summary = $"{groupIdName} is invalid",
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
                    }
                }
            };
        }
    }
}
