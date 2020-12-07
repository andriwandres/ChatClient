using Core.Application.Requests.AvailabilityStatus.Queries;
using Core.Domain.Resources.AvailabilityStatuses;
using Core.Domain.Resources.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.AvailabilityStatuses;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/availability-statuses")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages availability statuses")]
    public class AvailabilityStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvailabilityStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of availability statuses
        /// </summary>
        ///
        /// <remarks>
        /// Returns a list of availability statuses that describe the users current availability in the application
        /// </remarks>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// List of availability statuses
        /// </returns>
        ///
        /// <response code="200">
        /// Contains a list of availability statuses
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllAvailabilityStatusesOkExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<IEnumerable<AvailabilityStatusResource>>> GetAllAvailabilityStatuses(CancellationToken cancellationToken = default)
        {
            GetAllAvailabilityStatusesQuery query = new GetAllAvailabilityStatusesQuery();

            IEnumerable<AvailabilityStatusResource> availabilityStatuses = await _mediator.Send(query, cancellationToken);

            return Ok(availabilityStatuses);
        }
    }
}
