using AutoMapper;
using Core.Application.Requests.Groups.Commands;
using Core.Application.Requests.Groups.Queries;
using Core.Domain.Dtos.Groups;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Groups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Groups;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/groups")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages groups of users")]
    public class GroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new group
        /// </summary>
        ///
        /// <remarks>
        /// Creates a new group of users in the database and returns the newly created resource as well as a url for resource creation in the Location header
        /// </remarks>
        /// 
        /// <param name="model">
        /// Specifies information of the group to be created
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Created group resource
        /// </returns>
        ///
        /// <response code="201">
        /// Group has been created and is returned in the response
        /// </response>
        ///
        /// <response code="400">
        /// Group name is invalid
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPost]
        [Authorize]

        [SwaggerRequestExample(typeof(CreateGroupDto), typeof(CreateGroupRequestExample))]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreateGroupResponseExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateGroupValidationErrorResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<GroupResource>> CreateGroup([FromBody] CreateGroupDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateGroupCommand command = _mapper.Map<CreateGroupDto, CreateGroupCommand>(model);

            GroupResource group = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetGroupById), new { groupId = group.GroupId }, group);
        }

        /// <summary>
        /// Gets group information
        /// </summary>
        ///
        /// <remarks>
        /// Returns a single group by its ID
        /// </remarks>
        /// 
        /// <param name="groupId">
        /// ID of the group to get
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Group with given ID
        /// </returns>
        ///
        /// <response code="200">
        /// Contains group with given ID
        /// </response>
        ///
        /// <response code="404">
        /// A group with the given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{groupId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetGroupByIdResponseExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GroupNotFoundResponseExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<GroupResource>> GetGroupById([FromRoute] int groupId, CancellationToken cancellationToken = default)
        {
            GetGroupByIdQuery query = new GetGroupByIdQuery
            {
                GroupId = groupId
            };

            GroupResource group = await _mediator.Send(query, cancellationToken);

            if (group == null)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Group with ID '{groupId}' does not exist"
                });
            }

            return Ok(group);
        }

        [HttpPut("{groupId:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateGroup()
        {
            return NoContent();
        }

        [HttpDelete("{groupId:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteGroup()
        {
            return NoContent();
        }

        [HttpGet("{groupId:int}/memberships")]
        [Authorize]
        public async Task<ActionResult> GetMemberships()
        {
            return NoContent();
        }
    }
}
