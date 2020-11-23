using Core.Domain.Dtos.Friendships;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Friendships
{
    public class RequestFriendshipBadRequestExample : IExamplesProvider<ValidationErrorResource>
    {
        public ValidationErrorResource GetExamples()
        {
            const string addresseeIdName = nameof(RequestFriendshipBody.AddresseeId);

            return new ValidationErrorResource
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "One or multiple validation errors occurred",
                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        addresseeIdName, 
                        new []
                        {
                            $"'{addresseeIdName}' must be greater than 0"
                        }
                    }
                }
            };
        }
    }
}
