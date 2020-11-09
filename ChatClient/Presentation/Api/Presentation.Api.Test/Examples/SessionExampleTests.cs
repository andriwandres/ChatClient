using Presentation.Api.Examples.Session;
using Xunit;

namespace Presentation.Api.Test.Examples
{
    public class SessionExampleTests
    {
        [Fact]
        public void LoginRequestExample_ReturnsValidExample()
        {
            LoginRequestExample provider = new LoginRequestExample();

            Assert.NotNull(provider.GetExamples());
            Assert.NotEmpty(provider.GetExamples());
        }

        [Fact]
        public void LoginResponseExample_ReturnsValidExample()
        {
            LoginResponseExample provider = new LoginResponseExample();

            Assert.NotNull(provider.GetExamples());
        }
    }
}
