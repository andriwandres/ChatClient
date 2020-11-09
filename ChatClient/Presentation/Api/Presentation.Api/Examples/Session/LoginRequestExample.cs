using Core.Domain.Dtos.Session;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Session
{
    public class LoginRequestExample : IMultipleExamplesProvider<LoginUserDto>
    {
        public IEnumerable<SwaggerExample<LoginUserDto>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<LoginUserDto>
                {
                    Name = "UserName",
                    Summary = "Log in by user name",
                    Value = new LoginUserDto
                    {
                        UserNameOrEmail = "alfred_miller",
                        Password = "p455w0rd"
                    }
                },
                new SwaggerExample<LoginUserDto>
                {
                    Name = "Email",
                    Summary = "Log in by email address",
                    Value = new LoginUserDto
                    {
                        UserNameOrEmail = "alfred.miller@gmail.com",
                        Password = "p455w0rd"
                    }
                },
            };
        }
    }
}
