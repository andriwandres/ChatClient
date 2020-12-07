using Core.Application.Requests.Messages.Queries;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Messages;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/recipients")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages recipients")]
    public class RecipientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of messages with a recipient
        /// </summary>
        ///
        /// <remarks>
        /// Returns a list of messages that have been sent to or received by the given recipient.
        /// </remarks>
        /// 
        /// <param name="recipientId">
        /// The ID of the recipient to load messages from
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// List of messages with given recipient
        /// </returns>
        ///
        /// <response code="200">
        /// Contains a list of messages with given recipient
        /// </response>
        ///
        /// <response code="404">
        /// Recipient with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{recipientId:int}/messages")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetMessagesWithRecipientOkExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetMessagesWithRecipientNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<IEnumerable<ChatMessageResource>>> GetMessagesWithRecipient([FromRoute] int recipientId, CancellationToken cancellationToken = default)
        {
            // Check if the given recipient exists
            RecipientExistsQuery existsQuery = new RecipientExistsQuery { RecipientId = recipientId };

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Recipient with ID '{recipientId}' does not exist"
                });
            }
            
            // Get messages with given recipient
            GetMessagesWithRecipientQuery fetchQuery = new GetMessagesWithRecipientQuery{ RecipientId = recipientId };

            IEnumerable<ChatMessageResource> messages = await _mediator.Send(fetchQuery, cancellationToken);

            return Ok(messages);
        }
    }
}
