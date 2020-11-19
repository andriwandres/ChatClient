using Core.Domain.Dtos.Groups;
using Core.Domain.Resources.Errors;
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
using AutoMapper;
using Core.Application.Requests.Groups.Commands;
using Core.Domain.Resources.Groups;

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

        [HttpGet("{groupId:int}")]
        [Authorize]
        public async Task<ActionResult> GetGroupById()
        {
            return NoContent();
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
