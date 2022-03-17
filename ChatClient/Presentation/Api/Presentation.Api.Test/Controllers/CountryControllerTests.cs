using Core.Application.Requests.Countries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels;
using Xunit;

namespace Presentation.Api.Test.Controllers;

public class CountryControllerTests
{
    [Fact]
    public async Task GetCountries_ShouldGetAllCountries()
    {
        // Arrange
        IEnumerable<CountryViewModel> expectedCountries = new[]
        {
            new CountryViewModel {CountryId = 1},
            new CountryViewModel {CountryId = 2}
        };
            
        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCountries);

        CountryController controller = new CountryController(mediatorMock.Object);

        // Act
        ActionResult<IEnumerable<CountryViewModel>> response = await controller.GetCountries();

        // Assert
        OkObjectResult result = Assert.IsType<OkObjectResult>(response.Result);

        IEnumerable<CountryViewModel> actualCountries = (IEnumerable<CountryViewModel>) result.Value;

        Assert.Equal(2, actualCountries.Count());
    }
}