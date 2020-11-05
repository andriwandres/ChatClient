using AutoMapper;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Users;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Api.Examples.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Checks whether a given email exists in the database
        /// </summary>
        /// 
        /// <param name="model">
        /// Specifies the email address to query by
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// No content, since HEAD requests carry no response body
        /// </returns>
        ///
        /// <response code="200">
        /// Given email address exists in the database
        /// </response>
        ///
        /// <response code="400">
        /// Given email address is in an invalid format
        /// </response>
        ///
        /// <response code="404">
        /// Given email address does not exist in the database
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred on the server
        /// </response>
        [HttpHead("email")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EmailExists([FromQuery] EmailExistsDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmailExistsQuery query = _mapper.Map<EmailExistsDto, EmailExistsQuery>(model);

            bool exists = await _mediator.Send(query, cancellationToken);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Checks whether a given user name exists in the database
        /// </summary>
        /// 
        /// <param name="model">
        /// Specifies the user name to query by
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// No content, since HEAD requests carry no response body
        /// </returns>
        ///
        /// <response code="200">
        /// Given user name exists in the database
        /// </response>
        ///
        /// <response code="400">
        /// Given user name is in an invalid format
        /// </response>
        ///
        /// <response code="404">
        /// Given user name does not exist in the database
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred on the server
        /// </response>
        [HttpHead("username")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UserNameExists([FromQuery] UserNameExistsDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserNameExistsQuery query = _mapper.Map<UserNameExistsDto, UserNameExistsQuery>(model);

            bool exists = await _mediator.Send(query, cancellationToken);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
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
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(LoginCredentialsDto), typeof(LoginRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
        public async Task<ActionResult<AuthenticatedUser>> Login([FromBody] LoginCredentialsDto credentials, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginQuery query = _mapper.Map<LoginCredentialsDto, LoginQuery>(credentials);

            AuthenticatedUser user = await _mediator.Send(query, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Authenticates the current user, given a access token inside the Authorization request header
        /// </summary>
        ///
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// User information alongside access token
        /// </returns>
        ///
        /// <response code="200">
        /// Contains authenticated user alongside access token
        /// </response>
        ///
        /// <response code="400">
        /// Access token in authorization header is invalid or expired
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occured on the server
        /// </response>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AuthenticateResponseExample))]
        public async Task<ActionResult<AuthenticatedUser>> Authenticate(CancellationToken cancellationToken = default)
        {
            AuthenticateQuery query = new AuthenticateQuery();

            AuthenticatedUser user = await _mediator.Send(query, cancellationToken);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

    }
}
