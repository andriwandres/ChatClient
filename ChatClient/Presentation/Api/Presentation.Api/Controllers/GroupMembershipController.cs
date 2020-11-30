using AutoMapper;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Application.Requests.GroupMemberships.Queries;
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
    [Consumes(MediaTypeNames.Application.Json)]
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
        /// <param name="body">
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
        /// <response code="403">
        /// <para>1.) The user is already a member of this group</para>
        /// <para>2.) You are not permitted to create users in this group</para>
        /// </response>
        /// 
        /// <response code="404">
        /// <para>1.) Provided user does not exist</para>
        /// <para>2.) Provided group does not exist</para>
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

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(CreateMembershipForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(CreateMembershipNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<GroupMembershipResource>> CreateMembership([FromBody] CreateMembershipBody body, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the provided group exists
            GroupExistsQuery groupExistsQuery = new GroupExistsQuery { GroupId = body.GroupId };

            bool groupExists = await _mediator.Send(groupExistsQuery, cancellationToken);

            if (!groupExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Group with ID '{body.GroupId}' does not exist"
                });
            }
            
            // Check if the provided user exists
            UserExistsQuery userExistsQuery = new UserExistsQuery { UserId = body.UserId };

            bool userExists = await _mediator.Send(userExistsQuery, cancellationToken);

            if (!userExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"User with ID '{body.UserId}' does not exist"
                });
            }

            // Check if the current user is permitted to create memberships in this group
            CanCreateMembershipQuery canCreateQuery = new CanCreateMembershipQuery { GroupId = body.GroupId };

            bool canCreate = await _mediator.Send(canCreateQuery, cancellationToken);

            if (!canCreate)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You are not permitted to add users to this group. This privilege is only granted to administrators of the group"
                });
            }

            // Check if such a membership does not already exist
            MembershipCombinationExistsQuery membershipExistsQuery = _mapper.Map<CreateMembershipBody, MembershipCombinationExistsQuery>(body);

            bool membershipExists = await _mediator.Send(membershipExistsQuery, cancellationToken);

            if (membershipExists)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "This user is already a member of this group"
                });
            }

            CreateMembershipCommand createCommand = _mapper.Map<CreateMembershipBody, CreateMembershipCommand>(body);

            GroupMembershipResource membership = await _mediator.Send(createCommand, cancellationToken);

            return CreatedAtAction(nameof(GetMembershipById), new { membershipId = membership.GroupMembershipId }, membership);
        }

        /// <summary>
        /// Gets a single membership
        /// </summary>
        ///
        /// <remarks>
        /// Returns a single group membership by its ID
        /// </remarks>
        /// 
        /// <param name="membershipId">
        /// The ID of the membership to get
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Group membership that matches given ID
        /// </returns>
        ///
        /// <response code="200">
        /// Contains matching membership
        /// </response>
        ///
        /// <response code="404">
        /// Membership with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{membershipId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetMembershipByIdOkExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetMembershipByIdNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<GroupMembershipResource>> GetMembershipById([FromRoute] int membershipId, CancellationToken cancellationToken = default)
        {
            GetMembershipByIdQuery fetchQuery = new GetMembershipByIdQuery {GroupMembershipId = membershipId};

            GroupMembershipResource membership = await _mediator.Send(fetchQuery, cancellationToken);

            if (membership == null)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Membership with ID '{membershipId}' does not exist"
                });
            }

            return Ok(membership);
        }

        /// <summary>
        /// Updates a membership
        /// </summary>
        ///
        /// <remarks>
        /// Updates the admin status of an existing group membership
        /// </remarks>
        /// 
        /// <param name="membershipId">
        /// ID of the membership to update
        /// </param>
        /// 
        /// <param name="body">
        /// Specifies the admin status to be updated
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
        /// Request body failed model validation
        /// </response>
        ///
        /// <response code="403">
        /// <para>1.) User tried to update his own membership</para>
        /// <para>2.) User is not permitted to update this membership. Only administrators of a group can update memberships</para>
        /// </response>
        ///
        /// <response code="404">
        /// Membership with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPut("{membershipId:int}")]
        [Authorize]

        [SwaggerRequestExample(typeof(UpdateMembershipBody), typeof(UpdateMembershipBodyExample))]

        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateMembershipBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(UpdateMembershipForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UpdateMembershipNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> UpdateMembership([FromRoute] int membershipId, [FromBody] UpdateMembershipBody body, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if membership exists
            MembershipExistsQuery existsQuery = new MembershipExistsQuery {GroupMembershipId = membershipId};

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Membership with ID '{membershipId}' does not exist"
                });
            }

            // Check if the user wants to update himself
            IsOwnMembershipQuery isOwnMembershipQuery = new IsOwnMembershipQuery {GroupMembershipId = membershipId};

            bool isOwnMembership = await _mediator.Send(isOwnMembershipQuery, cancellationToken);

            if (isOwnMembership)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Cannot update your own membership"
                });
            }

            // Check if the current user is allowed to update the membership
            CanUpdateMembershipQuery canUpdateQuery = new CanUpdateMembershipQuery {GroupMembershipIdToUpdate = membershipId};

            bool canUpdate = await _mediator.Send(canUpdateQuery, cancellationToken);

            if (!canUpdate)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You are not permitted to mutate users in this group. This privilege is only granted to administrators of the group"
                });
            }

            // Update membership
            UpdateMembershipCommand updateCommand = new UpdateMembershipCommand
            {
                GroupMembershipId = membershipId,
                IsAdmin = body.IsAdmin != null && (bool) body.IsAdmin
            };

            await _mediator.Send(updateCommand, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Deletes a membership
        /// </summary>
        ///
        /// <remarks>
        /// Deletes a user from a group
        /// </remarks>
        /// 
        /// <param name="membershipId">
        /// ID of the membership to delete
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
        /// Deletion was successful
        /// </response>
        ///
        /// <response code="403">
        /// The user is not permitted to delete this user from this group
        /// </response>
        /// 
        /// <response code="404">
        /// Membership with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpDelete("{membershipId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(DeleteMembershipForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(DeleteMembershipNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> DeleteMembership([FromRoute] int membershipId, CancellationToken cancellationToken = default)
        {
            // Check if the membership exists
            MembershipExistsQuery existsQuery = new MembershipExistsQuery {GroupMembershipId = membershipId};

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Membership with ID '{membershipId}' does not exist"
                });
            }

            // Check if the user is permitted to delete
            CanDeleteMembershipQuery canDeleteQuery = new CanDeleteMembershipQuery { GroupMembershipIdToDelete = membershipId };
            bool canDelete = await _mediator.Send(canDeleteQuery, cancellationToken);

            if (!canDelete)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You are not permitted to delete users from this group. This privilege is only granted to administrators of the group"
                });
            }

            // Delete the membership
            DeleteMembershipCommand deleteCommand = new DeleteMembershipCommand {GroupMembershipId = membershipId};

            await _mediator.Send(deleteCommand, cancellationToken);

            return NoContent();
        }
    }
}
