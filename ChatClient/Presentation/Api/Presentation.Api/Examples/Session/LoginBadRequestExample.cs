using Core.Domain.Dtos.Session;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Session;

public class LoginBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const string passwordName = nameof(LoginBody.Password);
        const int passwordMinLength = 8;

        const string userNameOrEmailName = nameof(LoginBody.UserNameOrEmail);
        const int userNameMinLength = 2;

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>()
            {
                Name = "PasswordLength",
                Summary = "Password is to short",
                Value = new ValidationErrorViewModel
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
            new SwaggerExample<ValidationErrorViewModel>()
            {
                Name = "UserNameOrEmailLength",
                Summary = "UserName is to short",
                Value = new ValidationErrorViewModel
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
            new SwaggerExample<ValidationErrorViewModel>()
            {
                Name = "UserNameOrEmailPattern",
                Summary = "Email is in incorrect format",
                Value = new ValidationErrorViewModel
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