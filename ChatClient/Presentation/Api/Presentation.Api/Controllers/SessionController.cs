using AutoMapper;
using Core.Application.Requests.Session.Queries;
using Core.Domain.Dtos.Session;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Session;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/session")]
    [Produces("application/json")]
    [SwaggerTag("Manages sign-in session")]
    public class SessionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SessionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Signs the user in to a new session
        /// </summary>
        ///
        /// <remarks>
        /// Validates given user credentials and returns the user's information alongside a new valid access token
        /// </remarks>
        /// 
        /// <param name="credentials">
        /// User credentials to be validated
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// User information alongside new access token
        /// </returns>
        ///
        /// <response code="200">
        /// Login was successful. User information is returned
        /// </response>
        ///
        /// <response code="400">
        /// User credentials are in an incorrect format
        /// </response>
        ///
        /// <response code="401">
        /// UserName, e-mail and/or password were incorrect
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPut]
        [AllowAnonymous]

        [SwaggerRequestExample(typeof(LoginBody), typeof(LoginBodyExample))]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginOkExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(LoginBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(LoginUnauthorizedExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<AuthenticatedUserResource>> Login([FromBody] LoginBody credentials, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginQuery query = _mapper.Map<LoginBody, LoginQuery>(credentials);

            AuthenticatedUserResource user = await _mediator.Send(query, cancellationToken);

            if (user == null)
            {
                return Unauthorized(new ErrorResource
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "UserName, e-mail and/or password are incorrect"
                });
            }

            return Ok(user);
        }
    }
}
