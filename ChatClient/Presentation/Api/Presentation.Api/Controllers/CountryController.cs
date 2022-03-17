using Core.Application.Requests.Countries.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Countries;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels;
using Core.Domain.ViewModels.Errors;

namespace Presentation.Api.Controllers;

[ApiController]
[Route("api/countries")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Manages countries")]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a list of countries
    /// </summary>
    ///
    /// <remarks>
    /// Returns a list of all available countries
    /// </remarks>
    /// 
    /// <param name="cancellationToken">
    /// Notifies asynchronous operations to cancel ongoing work and release resources
    /// </param>
    /// 
    /// <returns>
    /// List of countries
    /// </returns>
    ///
    /// <response code="200">
    /// Contains list of countries
    /// </response>
    ///
    /// <response code="500">
    /// An unexpected error occurred
    /// </response>
    [HttpGet]
    [AllowAnonymous]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCountriesOkExample))]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorViewModel))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    public async Task<ActionResult<IEnumerable<CountryViewModel>>> GetCountries(CancellationToken cancellationToken = default)
    {
        GetCountriesQuery query = new GetCountriesQuery();

        IEnumerable<CountryViewModel> countries = await _mediator.Send(query, cancellationToken);

        return Ok(countries);
    }
}