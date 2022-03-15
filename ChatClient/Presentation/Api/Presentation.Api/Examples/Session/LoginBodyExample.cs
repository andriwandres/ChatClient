using Core.Domain.Dtos.Session;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Session;

public class LoginBodyExample : IMultipleExamplesProvider<LoginBody>
{
    public IEnumerable<SwaggerExample<LoginBody>> GetExamples()
    {
        return new[]
        {
            new SwaggerExample<LoginBody>
            {
                Name = "UserName",
                Summary = "Log in by user name",
                Value = new LoginBody
                {
                    UserNameOrEmail = "alfred_miller",
                    Password = "p4ssw0rd"
                }
            },
            new SwaggerExample<LoginBody>
            {
                Name = "Email",
                Summary = "Log in by email address",
                Value = new LoginBody
                {
                    UserNameOrEmail = "alfred.miller@gmail.com",
                    Password = "p4ssw0rd"
                }
            },
        };
    }
}