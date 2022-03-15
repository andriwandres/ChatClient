using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities;

public class FriendshipTests
{
    [Fact]
    public void FriendshipConstructor_ShouldInitializeStatusChangesNavigationProperty_WithEmptyCollection()
    {
        // Act
        Friendship friendship = new Friendship();

        // Assert
        Assert.Empty(friendship.StatusChanges);
    }
}