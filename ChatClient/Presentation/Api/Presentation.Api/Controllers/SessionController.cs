using AutoMapper;
using Core.Application.Requests.Session.Queries;
using Core.Domain.Dtos.Session;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(LoginUserDto), typeof(LoginRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
        public async Task<ActionResult<AuthenticatedUserResource>> Login([FromBody] LoginUserDto credentials, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginUserQuery query = _mapper.Map<LoginUserDto, LoginUserQuery>(credentials);

            AuthenticatedUserResource user = await _mediator.Send(query, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
