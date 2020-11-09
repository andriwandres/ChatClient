using Presentation.Api.Examples.Users;
using Xunit;

namespace Presentation.Api.Test.Examples
{
    public class UserExampleTests
    {
        [Fact]
        public void AuthenticateResponseExample_ReturnsValidExample()
        {
            AuthenticateResponseExample provider = new AuthenticateResponseExample();

            Assert.NotNull(provider.GetExamples());
        }

        [Fact]
        public void GetUserProfileResponseExample_ReturnsValidExample()
        {
            GetUserProfileResponseExample provider = new GetUserProfileResponseExample();

            Assert.NotNull(provider.GetExamples());
        }

        [Fact]
        public void RegisterUserRequestExample_ReturnsValidExample()
        {
            RegisterUserRequestExample provider = new RegisterUserRequestExample();

            Assert.NotNull(provider.GetExamples());
        }
    }
}
