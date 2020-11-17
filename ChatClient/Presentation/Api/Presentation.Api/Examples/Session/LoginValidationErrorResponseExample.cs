using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Session
{
    public class LoginValidationErrorResponseExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<ValidationErrorResource>()
                {
                    Name = "PasswordLength",
                    Summary = "Password is to short",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "Password", new [] { "The length of 'Password' must be at least 8 characters. You entered x characters." } }
                        }
                    }
                },
                new SwaggerExample<ValidationErrorResource>()
                {
                    Name = "UserNameOrEmailLength",
                    Summary = "UserName is to short",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "UserNameOrEmail", new [] { "The length of 'UserNameOrEmail' must be at least 2 characters. You entered x characters." } }
                        }
                    }
                },
                new SwaggerExample<ValidationErrorResource>()
                {
                    Name = "UserNameOrEmailPattern",
                    Summary = "Email is in incorrect format",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "UserNameOrEmail", new [] { "'UserNameOrEmail' is not in the correct format." } }
                        }
                    }
                },
            };
        }
    }
}
