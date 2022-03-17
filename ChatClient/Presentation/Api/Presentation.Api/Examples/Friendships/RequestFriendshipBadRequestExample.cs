using Core.Domain.Dtos.Friendships;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Friendships;

public class RequestFriendshipBadRequestExample : IExamplesProvider<ValidationErrorViewModel>
{
    public ValidationErrorViewModel GetExamples()
    {
        const string addresseeIdName = nameof(RequestFriendshipBody.AddresseeId);

        return new ValidationErrorViewModel
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "One or multiple validation errors occurred",
            Errors = new Dictionary<string, IEnumerable<string>>
            {
                {
                    addresseeIdName, 
                    new []
                    {
                        $"'{addresseeIdName}' must not be empty"
                    }
                }
            }
        };
    }
}