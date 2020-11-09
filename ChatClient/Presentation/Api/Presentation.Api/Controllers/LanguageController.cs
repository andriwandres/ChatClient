using Core.Application.Requests.Languages.Queries;
using Core.Domain.Dtos.Languages;
using Core.Domain.Resources.Languages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/languages")]
    [Produces("application/json")]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of all available languages
        /// </summary>
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LanguageResource>>> GetAllLanguages(CancellationToken cancellationToken = default)
        {
            GetAllLanguagesQuery query = new GetAllLanguagesQuery();

            IEnumerable<LanguageResource> languages = await _mediator.Send(query, cancellationToken);

            return Ok(languages);
        }

        /// <summary>
        /// Gets a list of translations for a specific language
        /// </summary>
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
        /// Language ID and/or pattern filter are invalid
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return NotFound();
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
