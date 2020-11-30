using AutoMapper;
using Core.Application.Requests.Friendships.Commands;
using Core.Application.Requests.Friendships.Queries;
using Core.Application.Requests.Users.Queries;
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
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/friendships")]
    [Consumes(MediaTypeNames.Application.Json)]
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
        /// <param name="body">
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
        /// <response code="404">
        /// Provided addressee user does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPost]
        [Authorize]

        [SwaggerRequestExample(typeof(RequestFriendshipBody), typeof(RequestFriendshipBodyExample))]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RequestFriendshipCreatedExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(RequestFriendshipBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(RequestFriendshipNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<FriendshipResource>> RequestFriendship([FromBody] RequestFriendshipBody body, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the current user id
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Check if requester + addressee id are the same
            if (requesterId == body.AddresseeId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You cannot create a friendship with yourself"
                });
            }

            // Check if the addressed user exists
            UserExistsQuery existsQuery = new UserExistsQuery { UserId = body.AddresseeId };

            bool userExists = await _mediator.Send(existsQuery, cancellationToken);

            if (!userExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"User with ID '{body.AddresseeId}' does not exist"
                });
            }

            // Check if there is already a friendship with the given user combination
            FriendshipCombinationExistsQuery combinationExistsQuery = new FriendshipCombinationExistsQuery
            {
                RequesterId = requesterId,
                AddresseeId = body.AddresseeId
            };

            bool combinationExists = await _mediator.Send(combinationExistsQuery, cancellationToken);

            if (combinationExists)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = $"There is already a friendship with user {body.AddresseeId}"
                });
            }

            // Create friendship
            RequestFriendshipCommand command = _mapper.Map<RequestFriendshipBody, RequestFriendshipCommand>(body);

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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetFriendshipByIdOkExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetFriendshipByIdNotFoundExample))]

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
        /// <remarks>
        /// Updates the friendship status of a given friendship
        /// </remarks>
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

        [SwaggerRequestExample(typeof(UpdateFriendshipStatusBody), typeof(UpdateFriendshipStatusBodyExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateFriendshipStatusBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UpdateFriendshipStatusNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> UpdateFriendshipStatus([FromRoute] int friendshipId, [FromBody] UpdateFriendshipStatusBody model, CancellationToken cancellationToken = default)
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

            UpdateFriendshipStatusCommand updateCommand = new UpdateFriendshipStatusCommand
            {
                FriendshipId = friendshipId,
                FriendshipStatusId = model.FriendshipStatusId
            };

            await _mediator.Send(updateCommand, cancellationToken);

            return NoContent();
        }
    }
}
