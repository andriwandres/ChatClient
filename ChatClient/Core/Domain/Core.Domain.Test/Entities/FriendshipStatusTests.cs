using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class FriendshipStatusTests
    {
        [Fact]
        public void FriendshipStatusConstructor_ShouldInitializeStatusChangesNavigationProperty_WithEmptyCollection()
        {
            // Act
            FriendshipStatus status = new FriendshipStatus();

            // Assert
            Assert.Empty(status.StatusChanges);
        }
    }
}
