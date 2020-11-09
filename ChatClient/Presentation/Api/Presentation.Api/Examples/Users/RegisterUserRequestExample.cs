using Core.Domain.Dtos.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Users
{
    public class RegisterUserRequestExample : IExamplesProvider<RegisterUserDto>
    {
        public RegisterUserDto GetExamples()
        {
            return new RegisterUserDto
            {
                UserName = "alfred_miller",
                Email = "alfred.miller@gmail.com",
                Password = "p4ssw0rd",
            };
        }
    }
}
