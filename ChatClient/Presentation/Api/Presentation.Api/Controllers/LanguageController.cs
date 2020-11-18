using Core.Application.Requests.Languages.Queries;
using Core.Domain.Dtos.Languages;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Languages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Languages;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/languages")]
    [Produces("application/json")]
    [SwaggerTag("Manages languages and translations")]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of all languages
        /// </summary>
        ///
        /// <remarks>
        /// Returns a list of all languages that are supported within this application
        /// </remarks>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// A list of available languages
        /// </returns>
        ///
        /// <response code="200">
        /// Contains a list of available languages
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet]
        [AllowAnonymous]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllLanguagesResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<IEnumerable<LanguageResource>>> GetAllLanguages(CancellationToken cancellationToken = default)
        {
            GetAllLanguagesQuery query = new GetAllLanguagesQuery();

            IEnumerable<LanguageResource> languages = await _mediator.Send(query, cancellationToken);

            return Ok(languages);
        }

        /// <summary>
        /// Gets a list of translations
        /// </summary>
        ///
        /// <remarks>
        /// Returns a list of translations for a given language. Translations within that language can also be filtered by pattern matching
        /// </remarks>
        /// 
        /// <param name="languageId">
        /// ID of the language to get translations from
        /// </param>
        /// 
        /// <param name="model">
        /// Specifies a pattern filter that can be applied to the translation key
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// A list of translations in the given language that satisfy the pattern filter
        /// </returns>
        ///
        /// <response code="200">
        /// Contains a list of translations for given language
        /// </response>
        ///
        /// <response code="400">
        /// Search pattern contains illegal characters
        /// </response>
        ///
        /// <response code="404">
        /// Language with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{languageId:int}/translations")]
        [AllowAnonymous]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetTranslationsByLanguageResponseExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GetTranslationsByLanguageValidationErrorResponseExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(LanguageNotFoundResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<IDictionary<string, string>>> GetTranslationsByLanguage([FromRoute] int languageId, [FromQuery] GetTranslationsByLanguageDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LanguageExistsQuery existsQuery = new LanguageExistsQuery { LanguageId = languageId };

            bool languageExists = await _mediator.Send(existsQuery, cancellationToken);

            if (!languageExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Language with ID '{languageId}' does not exist"
                });
            }

            GetTranslationsByLanguageQuery translationsQuery = new GetTranslationsByLanguageQuery
            {
                LanguageId = languageId,
                Pattern = model.Pattern
            };

            IDictionary<string, string> translations = await _mediator.Send(translationsQuery, cancellationToken);

            return Ok(translations);
        }
    }
}
