using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;

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

        [HttpGet("{recipientId:int}/messages")]
        [Authorize]
        public async Task<ActionResult> GetMessagesWithRecipient([FromRoute] int recipientId, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
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
            
            return NoContent();
        }
    }
}