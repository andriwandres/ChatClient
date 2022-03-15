using Core.Application.Services;
using Infrastructure.Shared.Services;
using System;
using Xunit;

namespace Infrastructure.Shared.Test.Services;

public class DateProviderTests
{
    [Fact]
    public void Now_ShouldReturnValidDate()
    {
        // Arrange
        IDateProvider dateProvider = new DateProvider();

        // Act
        DateTime now = dateProvider.Now();

        // Assert
        DateTime parsed = DateTime.Parse($"{now}");

        Assert.False(parsed == default);
    }

    [Fact]
    public void UtcNow_ShouldReturnValidDate()
    {
        // Arrange
        IDateProvider dateProvider = new DateProvider();

        // Act
        DateTime utcNow = dateProvider.UtcNow();

        // Assert
        DateTime parsed = DateTime.Parse($"{utcNow}");

        Assert.False(parsed == default);
    }
}