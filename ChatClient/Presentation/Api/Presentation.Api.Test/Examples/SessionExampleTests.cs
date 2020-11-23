using Presentation.Api.Examples.Session;
using Xunit;

namespace Presentation.Api.Test.Examples
{
    public class SessionExampleTests
    {
        [Fact]
        public void LoginRequestExample_ReturnsValidExample()
        {
            LoginBodyExample provider = new LoginBodyExample();

            Assert.NotNull(provider.GetExamples());
            Assert.NotEmpty(provider.GetExamples());
        }

        [Fact]
        public void LoginResponseExample_ReturnsValidExample()
        {
            LoginOkExample provider = new LoginOkExample();

            Assert.NotNull(provider.GetExamples());
        }
    }
}
