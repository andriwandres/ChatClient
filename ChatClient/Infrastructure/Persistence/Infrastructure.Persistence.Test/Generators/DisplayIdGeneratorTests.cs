using Infrastructure.Persistence.Generators;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Xunit;

namespace Infrastructure.Persistence.Test.Generators
{
    public class DisplayIdGeneratorTests
    {
        [Fact]
        public void Next_ShouldNotGenerateDisplayIdTwice()
        {
            // Arrange
            ValueGenerator<string> generator = new DisplayIdGenerator();

            // Act
            string firstId = generator.Next(null!);
            string secondId = generator.Next(null!);

            // Assert
            Assert.NotNull(firstId);
            Assert.NotNull(secondId);

            Assert.NotEqual(firstId, secondId);
        }

        [Fact]
        public void ShouldNotGenerateTemporaryValues()
        {
            // Arrange
            ValueGenerator generator = new DisplayIdGenerator();

            // Act
            bool generatesTemporaryValues = generator.GeneratesTemporaryValues;

            // Assert
            Assert.False(generatesTemporaryValues);
        }
    }
}
