using Core.Domain.Dtos.Session;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Session
{
    public class LoginRequestExample : IExamplesProvider<LoginUserDto>
    {
        public LoginUserDto GetExamples()
        {
            return new LoginUserDto
            {
                UserNameOrEmail = "alfred.miller@gmail.com",
                Password = "p455w0rd"
            };
        }
    }
}
