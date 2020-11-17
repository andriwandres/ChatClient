using Core.Application.Requests.Languages.Queries;
using Core.Domain.Resources.Languages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Dtos.Languages;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Presentation.Api.Test.Controllers
{
    public class LanguageControllerTests
    {
        [Fact]
        public async Task GetAllLanguages_ShouldReturnAllLanguages()
        {
            // Arrange
            IEnumerable<LanguageResource> expectedLanguages = new[]
            {
                new LanguageResource {LanguageId = 1}
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllLanguagesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedLanguages);

            LanguageController controller = new LanguageController(mediatorMock.Object);

            // Act
            ActionResult<IEnumerable<LanguageResource>> response = await controller.GetAllLanguages();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            IEnumerable<LanguageResource> languages = okResult.Value as IList<LanguageResource>;

            Assert.NotNull(languages);
            Assert.Single(languages);
            Assert.Equal(1, languages.First().LanguageId);
        }

        [Fact]
        public async Task GetTranslationsByLanguage_ShouldReturnBadRequestResult_WhenModelValidationFails()
        {
            // Arrange
            LanguageController controller = new LanguageController(null);

            controller.ModelState.AddModelError("", "");

            // Act
            ActionResult<IDictionary<string, string>> response = await controller.GetTranslationsByLanguage(0, new GetTranslationsByLanguageDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetTranslationsByLangauge_ShouldReturnNotFoundResult_WhenLanguageDoesNotExist()
        {
            // Arrange
            const int languageId = 8941;
            GetTranslationsByLanguageDto model = new GetTranslationsByLanguageDto();

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<LanguageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            LanguageController controller = new LanguageController(mediatorMock.Object);

            // Act
            ActionResult<IDictionary<string, string>> response =
                await controller.GetTranslationsByLanguage(languageId, model);

            // Assert
            NotFoundObjectResult result = Assert.IsType<NotFoundObjectResult>(response.Result);

            ErrorResource error = Assert.IsType<ErrorResource>(result.Value);

            Assert.Equal(StatusCodes.Status404NotFound, error.StatusCode);
        }

        [Fact]
        public async Task GetTranslationsByLanguage_ShouldReturnTranslations_WhenLanguageExists()
        {
            // Arrange
            const int languageId = 1;
            GetTranslationsByLanguageDto model = new GetTranslationsByLanguageDto
            {
                Pattern = "*"
            };

            IDictionary<string, string> expectedTranslations = new Dictionary<string, string>
            {
                { "key", "value" } 
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<LanguageExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetTranslationsByLanguageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTranslations);

            LanguageController controller = new LanguageController(mediatorMock.Object);

            // Act
            ActionResult<IDictionary<string, string>> response =
                await controller.GetTranslationsByLanguage(languageId, model);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            IDictionary<string, string> translations = (IDictionary<string, string>) okResult.Value;

            Assert.NotNull(translations);
            Assert.NotEmpty(translations);
            Assert.Single(translations);
        }
    }
}
