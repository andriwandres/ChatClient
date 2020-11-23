using Presentation.Api.Examples.Users;
using Xunit;

namespace Presentation.Api.Test.Examples
{
    public class UserExampleTests
    {
        [Fact]
        public void AuthenticateResponseExample_ReturnsValidExample()
        {
            AuthenticateOkExample provider = new AuthenticateOkExample();

            Assert.NotNull(provider.GetExamples());
        }

        [Fact]
        public void GetUserProfileResponseExample_ReturnsValidExample()
        {
            GetUserProfileOkExample provider = new GetUserProfileOkExample();

            Assert.NotNull(provider.GetExamples());
        }

        [Fact]
        public void RegisterUserRequestExample_ReturnsValidExample()
        {
            CreateAccountBodyExample provider = new CreateAccountBodyExample();

            Assert.NotNull(provider.GetExamples());
        }
    }
}
