using Core.Domain.Dtos.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Users;

public class CreateAccountBodyExample : IExamplesProvider<CreateAccountBody>
{
    public CreateAccountBody GetExamples()
    {
        return new CreateAccountBody
        {
            UserName = "alfred_miller",
            Email = "alfred.miller@gmail.com",
            Password = "p4ssw0rd",
        };
    }
}