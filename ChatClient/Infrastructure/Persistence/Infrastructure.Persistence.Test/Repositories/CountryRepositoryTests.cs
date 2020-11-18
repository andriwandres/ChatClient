using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class CountryRepositoryTests
    {
        [Fact]
        public async Task GetAll_ShouldGetAllCountries()
        {
            // Arrange
            IEnumerable<Country> expectedCountries = new []
            {
                new Country { CountryId = 1 }
            };

            Mock<DbSet<Country>> countryDbSetMock = expectedCountries
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Countries)
                .Returns(countryDbSetMock.Object);

            CountryRepository repository = new CountryRepository(contextMock.Object);

            // Act
            IEnumerable<Country> actualCountries = await repository
                .GetAll()
                .ToListAsync();

            // Assert
            Assert.Single(actualCountries);
            Assert.Equal(1, actualCountries.First().CountryId);
        }
    }
}
