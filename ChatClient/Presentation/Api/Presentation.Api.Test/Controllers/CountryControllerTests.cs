using Core.Application.Requests.Countries.Queries;
using Core.Domain.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class CountryControllerTests
    {
        [Fact]
        public async Task GetCountries_ShouldGetAllCountries()
        {
            // Arrange
            IEnumerable<CountryResource> expectedCountries = new[]
            {
                new CountryResource {CountryId = 1},
                new CountryResource {CountryId = 2}
            };
            
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCountries);

            CountryController controller = new CountryController(mediatorMock.Object);

            // Act
            ActionResult<IEnumerable<CountryResource>> response = await controller.GetCountries();

            // Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

            IEnumerable<CountryResource> actualCountries = (IEnumerable<CountryResource>) result.Value;

            Assert.Equal(2, actualCountries.Count());
        }
    }
}
