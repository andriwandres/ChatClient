using Core.Domain.Dtos.GroupMemberships;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.GroupMemberships;

public class UpdateMembershipBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const string isAdminName = nameof(UpdateMembershipBody.IsAdmin);

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = $"{isAdminName}Null",
                Summary = $"'{isAdminName}' is null",
                Value = new ValidationErrorViewModel
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