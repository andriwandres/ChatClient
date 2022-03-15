﻿using AutoMapper;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Application.Requests.Groups.Commands;
using Core.Application.Requests.Groups.Queries;
using Core.Domain.Dtos.Groups;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.GroupMemberships;
using Core.Domain.Resources.Groups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.GroupMemberships;
using Presentation.Api.Examples.Groups;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers;

[ApiController]
[Route("api/groups")]
[Consumes(MediaTypeNames.Application.Json)]
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

    [SwaggerRequestExample(typeof(CreateGroupBody), typeof(CreateGroupBodyExample))]

    [ProducesResponseType(StatusCodes.Status201Created)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreateGroupCreatedExample))]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateGroupBadRequestExample))]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    public async Task<ActionResult<GroupResource>> CreateGroup([FromBody] CreateGroupBody model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        CreateGroupCommand command = _mapper.Map<CreateGroupBody, CreateGroupCommand>(model);

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
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetGroupByIdOkExample))]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetGroupByIdNotFoundExample))]

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

    /// <summary>
    /// Updates a group
    /// </summary>
    ///
    /// <remarks>
    /// Updates a groups information
    /// </remarks>
    /// 
    /// <param name="groupId">
    /// ID of the group to update
    /// </param>
    /// 
    /// <param name="model">
    /// Specifies the groups information to update
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
    /// Request body is invalid
    /// </response>
    ///
    /// <response code="404">
    /// Group with given ID does not exist
    /// </response>
    ///
    /// <response code="500">
    /// An unexpected error occurred
    /// </response>
    [HttpPut("{groupId:int}")]
    [Authorize]

    [SwaggerRequestExample(typeof(UpdateGroupBody), typeof(UpdateGroupBodyExample))]

    [ProducesResponseType(StatusCodes.Status204NoContent)]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateGroupBadRequestExample))]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UpdateGroupNotFoundExample))]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    public async Task<ActionResult> UpdateGroup([FromRoute] int groupId, [FromBody] UpdateGroupBody model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        GroupExistsQuery existsQuery = new GroupExistsQuery { GroupId = groupId };

        bool exists = await _mediator.Send(existsQuery, cancellationToken);

        if (!exists)
        {
            return NotFound(new ErrorResource
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Group with ID '{groupId}' does not exist"
            });
        }

        UpdateGroupCommand updateCommand = new UpdateGroupCommand
        {
            GroupId = groupId,
            Name = model.Name,
            Description = model.Description
        };

        await _mediator.Send(updateCommand, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes a group
    /// </summary>
    ///
    /// <remarks>
    /// Deletes a group from the database, given the ID
    /// </remarks>
    /// 
    /// <param name="groupId">
    /// ID of the group to delete
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
    /// <response code="404">
    /// Group with given ID does not exist
    /// </response>
    ///
    /// <response code="500">
    /// An unexpected error occurred
    /// </response>
    [HttpDelete("{groupId:int}")]
    [Authorize]

    [ProducesResponseType(StatusCodes.Status204NoContent)]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(DeleteGroupNotFoundExample))]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    public async Task<ActionResult> DeleteGroup([FromRoute] int groupId, CancellationToken cancellationToken = default)
    {
        GroupExistsQuery existsQuery = new GroupExistsQuery { GroupId = groupId };

        bool exists = await _mediator.Send(existsQuery, cancellationToken);

        if (!exists)
        {
            return NotFound(new ErrorResource
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Group with ID '{groupId}' does not exist"
            });
        }

        DeleteGroupCommand deleteCommand = new DeleteGroupCommand { GroupId = groupId };

        await _mediator.Send(deleteCommand, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Gets all memberships of a group
    /// </summary>
    ///
    /// <remarks>
    /// Returns a list of all memberships of a group, given the groups ID
    /// </remarks>
    /// 
    /// <param name="groupId">
    /// ID of the group to fetch the members from
    /// </param>
    /// 
    /// <param name="cancellationToken">
    /// Notifies asynchronous operations to cancel ongoing work and release resources
    /// </param>
    /// 
    /// <returns>
    /// List of memberships in given group
    /// </returns>
    ///
    /// <response code="200">
    /// Contains a list of memberships within the given group
    /// </response>
    ///
    /// <response code="404">
    /// The group with given ID does not exist
    /// </response>
    ///
    /// <response code="500">
    /// An unexpected error occurred
    /// </response>
    [HttpGet("{groupId:int}/memberships")]
    [Authorize]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetMembershipsByGroupOkExample))]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetMembershipsByGroupNotFoundExample))]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    public async Task<ActionResult<IEnumerable<GroupMembershipResource>>> GetMembershipsByGroup([FromRoute] int groupId, CancellationToken cancellationToken = default)
    {
        GroupExistsQuery existsQuery = new GroupExistsQuery { GroupId = groupId };

        bool exists = await _mediator.Send(existsQuery, cancellationToken);

        if (!exists)
        {
            return NotFound(new ErrorResource
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Group with ID '{groupId}' does not exist"
            });
        }

        GetMembershipsByGroupQuery membershipsQuery = new GetMembershipsByGroupQuery { GroupId = groupId };

        IEnumerable<GroupMembershipResource> memberships = await _mediator.Send(membershipsQuery, cancellationToken);

        return Ok(memberships);
    }
}