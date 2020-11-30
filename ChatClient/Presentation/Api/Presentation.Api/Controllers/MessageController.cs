using AutoMapper;
using Core.Application.Requests.Messages.Queries;
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
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages chat messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MessageController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SendMessage(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }

        /// <summary>
        /// Gets information about a message
        /// </summary>
        ///
        /// <remarks>
        /// Gets detailed information about a single message, given its unique ID.
        /// </remarks>
        /// 
        /// <param name="messageId">
        /// The ID of the message to get
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Message with given ID
        /// </returns>
        ///
        /// <response code="200">
        /// Contains message with given ID
        /// </response>
        ///
        /// <response code="403">
        /// The user is not permitted to access the given message.
        /// </response>
        ///
        /// <response code="404">
        /// Message with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{messageId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetMessageByIdOkExample))]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(GetMessageByIdForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetMessageByIdNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<MessageResource>> GetMessageById([FromRoute] int messageId, CancellationToken cancellationToken = default)
        {
            // Check if the message exists
            MessageExistsQuery existsQuery = new MessageExistsQuery { MessageId = messageId };

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Message with ID '{messageId}' does not exist"
                });
            }

            // Check if the user is allowed to see the message
            CanAccessMessageQuery canAccessQuery = new CanAccessMessageQuery { MessageId = messageId };

            bool canAccess = await _mediator.Send(canAccessQuery, cancellationToken);

            if (!canAccess)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "The user is not permitted to see this message. He may only see messages that he received or wrote himself"
                });
            }

            // Get the message
            GetMessageByIdQuery fetchQuery = new GetMessageByIdQuery { MessageId = messageId };

            MessageResource message = await _mediator.Send(fetchQuery, cancellationToken);

            return Ok(message);
        }

        [HttpPut("{messageId:int}")]
        [Authorize]
        public async Task<ActionResult> EditMessage(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }

        [HttpDelete("{messageId:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteMessage(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }
    }
}
