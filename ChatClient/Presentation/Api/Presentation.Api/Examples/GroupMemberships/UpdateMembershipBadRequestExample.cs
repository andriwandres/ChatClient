using Core.Domain.Dtos.GroupMemberships;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.GroupMemberships
{
    public class UpdateMembershipBadRequestExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            const string isAdminName = nameof(UpdateMembershipBody.IsAdmin);

            return new[]
            {
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = $"{isAdminName}Null",
                    Summary = $"'{isAdminName}' is null",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                isAdminName,
                                new[]
                                {
                                    $"'{isAdminName}' must not be null"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
