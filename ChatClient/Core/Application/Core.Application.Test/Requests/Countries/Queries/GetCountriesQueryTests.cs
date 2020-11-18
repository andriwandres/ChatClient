using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Countries.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Countries.Queries
{
    public class GetCountriesQueryTests
    {
        [Fact]
        public async Task GetCountriesQueryHandler_ReturnsCountries()
        {
            // Arrange
            GetCountriesQuery request = new GetCountriesQuery();

            IEnumerable<Country> expectedCountries = new[]
            {
                new Country { CountryId = 1 },
                new Country { CountryId = 2 },
            };

            Mock<IQueryable<Country>> queryableMock = expectedCountries
                .AsQueryable()
                .BuildMock();

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Countries.GetAll())
                .Returns(queryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Country, CountryResource>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            GetCountriesQuery.Handler handler = new GetCountriesQuery.Handler(mapperMock, unitOfWorkMock.Object);

            // Act
            IEnumerable<CountryResource> actualCountries = await handler.Handle(request);

            // Assert
            Assert.Equal(2, actualCountries.Count());
        }
    }
}
