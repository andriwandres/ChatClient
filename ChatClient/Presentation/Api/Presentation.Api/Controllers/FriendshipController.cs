using AutoMapper;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Requests.Friendships.Queries;
using Core.Domain.Dtos.Friendships;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Friendships;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Friendships;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/friendships")]
    [Produces(MediaTypeNames.Application.Json)]
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
        /// <response code="201">
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

        [SwaggerRequestExample(typeof(RequestFriendshipDto), typeof(RequestFriendshipRequestExample))]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(FriendshipResponseExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(RequestFriendshipValidationErrorResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<FriendshipResource>> RequestFriendship([FromBody] RequestFriendshipDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RequestFriendshipCommand command = _mapper.Map<RequestFriendshipDto, RequestFriendshipCommand>(model);

            FriendshipResource friendship = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetFriendshipById), new { friendshipId = friendship.FriendshipId }, friendship);
        }

        /// <summary>
        /// Gets a single friendship
        /// </summary>
        ///
        /// <remarks>
        /// Returns a single friendship according to the provided friendship ID
        /// </remarks>
        /// 
        /// <param name="friendshipId">
        /// ID of the friendship to get
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Friendship instance
        /// </returns>
        ///
        /// <response code="200">
        /// Contains the friendship
        /// </response>
        ///
        /// <response code="404">
        /// Friendship with the provided ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{friendshipId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(FriendshipResponseExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(FriendshipNotFoundResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<FriendshipResource>> GetFriendshipById([FromRoute] int friendshipId, CancellationToken cancellationToken = default)
        {
            GetFriendshipByIdQuery query = new GetFriendshipByIdQuery
            {
                FriendshipId = friendshipId
            };

            FriendshipResource friendship = await _mediator.Send(query, cancellationToken);

            if (friendship == null)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Friendship with ID '{friendshipId}' does not exist"
                });
            }

            return Ok(friendship);
        }

        /// <summary>
        /// Updates a friendships status
        /// </summary>
        ///
        /// <param name="friendshipId">
        /// ID of the friendship to update
        /// </param>
        /// 
        /// <param name="model">
        /// Specifies the updated friendship status
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// No content
        /// </returns>
        ///
        /// <response code="204">
        /// Update was successful
        /// </response>
        ///
        /// <response code="400">
        /// Updated friendship status in request body is invalid
        /// </response>
        /// 
        /// <response code="404">
        /// Friendship with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPut("{friendshipId:int}")]
        [Authorize]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [SwaggerRequestExample(typeof(UpdateFriendshipStatusDto), typeof(UpdateFriendshipStatusRequestExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateFriendshipStatusValidationErrorResponseExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(FriendshipNotFoundResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> UpdateFriendshipStatus([FromRoute] int friendshipId, [FromBody] UpdateFriendshipStatusDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FriendshipExistsQuery existsQuery = new FriendshipExistsQuery { FriendshipId = friendshipId };

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Friendship with ID '{friendshipId}' does not exist"
                });
            }

            UpdateFriendshipStatusCommand updateCommand = _mapper.Map<UpdateFriendshipStatusDto, UpdateFriendshipStatusCommand>(model);

            await _mediator.Send(updateCommand, cancellationToken);

            return NoContent();
        }
    }
}
