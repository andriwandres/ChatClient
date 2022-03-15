using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities;

public class RecipientTests
{
    [Fact]
    public void RecipientConstructor_ShouldInitializeReceivedMessagesNavigationProperty_WithEmptyCollection()
    {
        // Act
        Recipient recipient = new Recipient();

        // Assert
        Assert.Empty(recipient.ReceivedMessages);
    }

    [Fact]
    public void RecipientConstructor_ShouldInitializePinsNavigationProperty_WithEmptyCollection()
    {
        // Act
        Recipient recipient = new Recipient();

        // Assert
        Assert.Empty(recipient.Pins);
    }

    [Fact]
    public void RecipientConstructor_ShouldInitializeArchivesNavigationProperty_WithEmptyCollection()
    {
        // Act
        Recipient recipient = new Recipient();

        // Assert
        Assert.Empty(recipient.Archives);
    }
}