using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class EmojiTests
    {
        [Fact]
        public void EmojiConstructor_ShouldInitializeReactionsNavigationProperty_WithEmptyCollection()
        {
            // Act
            Emoji emoji = new Emoji();

            // Assert
            Assert.Empty(emoji.Reactions);
        }
    }
}
