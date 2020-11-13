using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Requests.Friendships.Commands;
using Core.Domain.Dtos.Friendships;
using Core.Domain.Resources.Friendships;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/friendships")]
    [Produces("application/json")]
    [SwaggerTag("Manages friendships between users")]
    public class FriendshipController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public FriendshipController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Requests a new friendship
        /// </summary>
        ///
        /// <remarks>
        /// Creates a new friendship between two users
        /// </remarks>
        /// 
        /// <param name="model">
        /// Specifies the user ID of the addresse to create the new friendship with
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Created friendship
        /// </returns>
        ///
        /// <response code="200">
        /// Contains the created friendship
        /// </response>
        ///
        /// <response code="400">
        /// Request body failed validation or the user combination for this friendship already exists
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RequestFriendship([FromBody] RequestFriendshipDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RequestFriendshipCommand command = _mapper.Map<RequestFriendshipDto, RequestFriendshipCommand>(model);

            FriendshipResource friendship = await _mediator.Send(command, cancellationToken);

            return Ok(friendship);
        }

        [HttpPut("{friendshipId:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateFriendshipStatus()
        {
            return NoContent();
        }

        [HttpDelete("{friendshipId:int}")]
        [Authorize]
        public async Task<ActionResult> RemoveFriendship()
        {
            return NoContent();
        }
    }
}
