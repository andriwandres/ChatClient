using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities;

public class MessageTests
{
    [Fact]
    public void MessageConstructor_ShouldInitializeReactionsNavigationProperty_WithEmptyCollection()
    {
        // Act
        Message message = new Message();

        // Assert
        Assert.Empty(message.Reactions);
    }

    [Fact]
    public void MessageConstructor_ShouldInitializeMessageRecipientsNavigationProperty_WithEmptyCollection()
    {
        // Act
        Message message = new Message();

        // Assert
        Assert.Empty(message.MessageRecipients);
    }
}