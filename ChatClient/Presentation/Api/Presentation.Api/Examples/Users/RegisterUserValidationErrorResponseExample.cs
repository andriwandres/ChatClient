using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Users
{
    public class RegisterUserValidationErrorResponseExample : IMultipleExamplesProvider<ValidationErrorResource>
    {
        public IEnumerable<SwaggerExample<ValidationErrorResource>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "EmailPattern",
                    Summary = "Email is invalid",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "Email", new []{ "'Email' is not a valid email address." }}
                        }
                    }
                },
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "UserNameLength",
                    Summary = "UserName is too short",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "UserName", new []{ "'The length of 'User Name' must be at least 2 characters. You entered x characters." }}
                        }
                    }
                },
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "UserNamePattern",
                    Summary = "UserName is invalid",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "UserName", new []{ "'User Name' is not in the correct format." }}
                        }
                    }
                },
                new SwaggerExample<ValidationErrorResource>
                {
                    Name = "PasswordLength",
                    Summary = "Password is too short",
                    Value = new ValidationErrorResource
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "One or multiple validation errors occurred",
                        Errors = new Dictionary<string, IEnumerable<string>>
                        {
                            { "Password", new []{ "The length of 'Password' must be at least 8 characters. You entered x characters." }}
                        }
                    }
                },
            };
        }
    }
}
