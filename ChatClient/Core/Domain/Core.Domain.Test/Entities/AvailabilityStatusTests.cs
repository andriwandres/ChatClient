using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class AvailabilityStatusTests
    {
        [Fact]
        public void AvailabilityStatusConstructor_ShouldInitializeAvailablitiesNavigationProperty_WithEmptyCollection()
        {
            // Act
            AvailabilityStatus status = new AvailabilityStatus();

            // Assert
            Assert.Empty(status.Availabilities);
        }
    }
}
