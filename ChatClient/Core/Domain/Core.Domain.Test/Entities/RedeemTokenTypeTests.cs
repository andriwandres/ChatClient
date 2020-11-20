using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class RedeemTokenTypeTests
    {
        [Fact]
        public void RedeemTokenTypeConstructor_ShouldInitializeTokensNavigationProperty_WithEmptyCollection()
        {
            // Act
            RedeemTokenType type = new RedeemTokenType();

            // Assert
            Assert.Empty(type.Tokens);
        }
    }
}
