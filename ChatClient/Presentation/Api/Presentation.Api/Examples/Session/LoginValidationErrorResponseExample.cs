using Core.Domain.Dtos.Session;
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
            const string passwordName = nameof(LoginUserDto.Password);
            const int passwordMinLength = 8;

            const string userNameOrEmailName = nameof(LoginUserDto.UserNameOrEmail);
            const int userNameMinLength = 2;

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
                            {
                                passwordName,
                                new []
                                {
                                    $"'{passwordName}' must be at least {passwordMinLength} characters long. You entered xxx characters"
                                }
                            }
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
                            {
                                userNameOrEmailName, 
                                new []
                                {
                                    $"'{userNameOrEmailName}' must be at least {userNameMinLength} characters long. You entered xxx characters"
                                }
                            }
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
                            { 
                                userNameOrEmailName, 
                                new []
                                {
                                    $"'{userNameOrEmailName}' must be a valid e-mail address, in case e-mail is the preffered login value"
                                }
                            }
                        }
                    }
                },
            };
        }
    }
}
