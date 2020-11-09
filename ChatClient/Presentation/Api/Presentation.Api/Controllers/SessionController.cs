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
        
        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
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

            LoginUserQuery userQuery = new LoginUserQuery
            {
                UserNameOrEmail = credentials.UserNameOrEmail,
                Password = credentials.Password
            };

            AuthenticatedUserResource user = await _mediator.Send(userQuery, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
