using Core.Domain.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Examples.Users;

public class CreateAccountBadRequestExample : IMultipleExamplesProvider<ValidationErrorViewModel>
{
    public IEnumerable<SwaggerExample<ValidationErrorViewModel>> GetExamples()
    {
        const string emailName = nameof(CreateAccountBody.Email);
        const string userNameName = nameof(CreateAccountBody.UserName);
        const string passwordName = nameof(CreateAccountBody.Password);

        const int userNameMinLength = 2;
        const int passwordMinLength = 8;

        return new[]
        {
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "EmailPattern",
                Summary = "Email is invalid",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            emailName, 
                            new []
                            {
                                $"'{emailName}' has an invalid e-mail address format"
                            }
                        }
                    }
                }
            },
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "UserNameLength",
                Summary = "UserName is too short",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            userNameName,
                            new []
                            {
                                $"'{userNameName}' must be at least {userNameMinLength} characters long. You entered xxx characters"
                            }
                        }
                    }
                }
            },
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "UserNamePattern",
                Summary = "UserName is invalid",
                Value = new ValidationErrorViewModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or multiple validation errors occurred",
                    Errors = new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            userNameName, 
                            new []
                            {
                                $"'{userNameName}' contains illegal characters. Use only alphanumeric characters including underscores"
                            }
                        }
                    }
                }
            },
            new SwaggerExample<ValidationErrorViewModel>
            {
                Name = "PasswordLength",
                Summary = "Password is too short",
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
        };
    }
}