using AutoMapper;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Users;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples.Users;
using Swashbuckle.AspNetCore.Filters;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Requests.Session.Queries;
using Core.Domain.Dtos.Session;
using Presentation.Api.Examples.Session;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/session")]
    [Produces("application/json")]
    public class SessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public SessionController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Validates login credentials and returns the users information with a new access token
        /// </summary>
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
        public async Task<ActionResult<AuthenticatedUser>> Login([FromBody] LoginUserDto credentials, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginUserQuery userQuery = _mapper.Map<LoginUserDto, LoginUserQuery>(credentials);

            AuthenticatedUser user = await _mediator.Send(userQuery, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
