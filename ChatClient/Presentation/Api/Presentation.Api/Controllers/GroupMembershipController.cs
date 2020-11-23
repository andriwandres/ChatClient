using AutoMapper;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Application.Requests.Groups.Queries;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.GroupMemberships;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.GroupMemberships;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.GroupMemberships;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/group-memberships")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages memberships of groups")]
    public class GroupMembershipController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupMembershipController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new group membership
        /// </summary>
        ///
        /// <remarks>
        /// Adds a new user to the list of members of a group
        /// </remarks>
        /// 
        /// <param name="model">
        /// Specifies information about the membership to create
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Created membership
        /// </returns>
        ///
        /// <response code="201">
        /// Creation was successful. Response contains created membership
        /// </response>
        ///
        /// <response code="400">
        /// Request body was invalid
        /// </response>
        ///
        /// <response code="404">
        ///     <para>1.) Provided user does not exist</para>
        ///     <para>2.) Provided group does not exist</para>
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPost]
        [Authorize]

        [SwaggerRequestExample(typeof(CreateMembershipBody), typeof(CreateMembershipBodyExample))]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreateMembershipCreatedExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateMembershipBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(CreateMembershipNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<GroupMembershipResource>> CreateMembership([FromBody] CreateMembershipBody model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the provided group exists
            GroupExistsQuery groupExistsQuery = new GroupExistsQuery { GroupId = model.GroupId };

            bool groupExists = await _mediator.Send(groupExistsQuery, cancellationToken);

            if (!groupExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Group with ID '{model.GroupId}' does not exist"
                });
            }
            
            // Check if the provided user exists
            UserExistsQuery userExistsQuery = new UserExistsQuery { UserId = model.UserId };

            bool userExists = await _mediator.Send(userExistsQuery, cancellationToken);

            if (!userExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"User with ID '{model.UserId}' does not exist"
                });
            }

            CreateMembershipCommand createCommand = _mapper.Map<CreateMembershipBody, CreateMembershipCommand>(model);

            GroupMembershipResource membership = await _mediator.Send(createCommand, cancellationToken);

            return CreatedAtAction(nameof(GetMembershipById), new { membershipId = membership.GroupMembershipId }, membership);
        }

        [HttpGet("{membershipId:int}")]
        [Authorize]
        public async Task<ActionResult<GroupMembershipResource>> GetMembershipById([FromRoute] int membershipId, CancellationToken cancellationToken = default)
        {
            return NoContent();
        }

        [HttpPut("{membershipId:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateMembership()
        {
            return NoContent();
        }

        [HttpDelete("{membershipId:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteMembership()
        {
            return NoContent();
        }
    }
}
