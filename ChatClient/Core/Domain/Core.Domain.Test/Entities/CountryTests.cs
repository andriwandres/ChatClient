using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities;

public class CountryTests
{
    [Fact]
    public void CountryConstructor_ShouldInitializeUsersNavigationProperty_WithEmptyCollection()
    {
        // Act
        Country country = new Country();

        // Assert
        Assert.Empty(country.Users);
    }
}