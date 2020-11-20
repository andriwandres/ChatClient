using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class GroupTests
    {
        [Fact]
        public void GroupConstructor_ShouldInitializeMembershipsNavigationProperty_WithEmptyCollection()
        {
            // Act
            Group group = new Group();

            // Assert
            Assert.Empty(group.Memberships);
        }
    }
}
