using Core.Application.Requests.Users.Commands;
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

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new user account based on given user information
        /// </summary>
        /// 
        /// <param name="credentials">
        /// Specifies the user credentials to be used for the new user account
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Location of where to fetch the created user data ('Location' header)
        /// </returns>
        ///
        /// <response code="201">
        /// Contains the location of where to fetch the created user data inside the 'Location' header
        /// </response>
        ///
        /// <response code="400">
        /// Provided user credentials are in an invalid format or a user with provided credentials already exists
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred on the server
        /// </response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto credentials, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegisterUserCommand command = new RegisterUserCommand
            {
                UserName = credentials.UserName,
                Email = credentials.Email,
                Password = credentials.Password
            };

            int id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetUserProfile), new { id }, null);
        }

        /// <summary>
        /// Returns a users profile information
        /// </summary>
        /// 
        /// <param name="userId">
        /// ID of the user to search by
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// User profile information
        /// </returns>
        /// 
        /// <response code="200">
        /// Contains user profile information
        /// </response>
        /// 
        /// <response code="400">
        /// The provided user ID is in an invalid format
        /// </response>
        /// 
        /// <response code="404">
        /// The user with the given ID could not be found
        /// </response>
        /// 
        /// <response code="500">
        /// An unexpected error occurred on the server
        /// </response>
        [HttpGet("{userId:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserProfileResource>> GetUserProfile([FromRoute] int userId, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GetUserProfileQuery query = new GetUserProfileQuery
            {
                UserId = userId
            };

            UserProfileResource userProfile = await _mediator.Send(query, cancellationToken);

            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
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

            EmailExistsQuery query = new EmailExistsQuery
            {
                Email = model.Email
            };

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

            UserNameExistsQuery query = new UserNameExistsQuery
            {
                UserName = model.UserName
            };

            bool exists = await _mediator.Send(query, cancellationToken);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
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
        public async Task<ActionResult<AuthenticatedUserResource>> Authenticate(CancellationToken cancellationToken = default)
        {
            AuthenticateQuery query = new AuthenticateQuery();

            AuthenticatedUserResource user = await _mediator.Send(query, cancellationToken);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

    }
}
