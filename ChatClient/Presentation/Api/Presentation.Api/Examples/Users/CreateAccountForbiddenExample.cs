﻿using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Users;

public class CreateAccountForbiddenExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = "A user with the same user name or email already exists. Please use different credentials for creating an account"
        };
    }
}