using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.Auth;
using ChatClient.Core.Models.ViewModels.Auth;
using ChatClient.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatClient.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _mapper = mapper;
            _authService = authService;
        }

        /// <summary>
        ///     Authenticates the User with a valid Access Token in the Authorization Header
        /// </summary>
        /// <returns>
        ///     User Information of the User with the valid Token
        /// </returns>
        [Authorize]
        [HttpGet("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthenticatedUser>> Authenticate()
        {
            (User user, string token) = await _authService.Authenticate();

            AuthenticatedUser authenticatedUser = _mapper.Map<User, AuthenticatedUser>(user, options =>
            {
                options.Items.Add("Token", token);
            });

            return Ok(authenticatedUser);
        }

        /// <summary>
        ///     Checks the Email Availiability of a given Email Address in the Database.
        /// </summary>
        /// <param name="query">
        ///     Query Parameter including the Email Address to query by
        /// </param>
        /// <returns>
        ///     True, if Email Address is already taken
        ///     False, if Email is free to use
        /// </returns>
        [AllowAnonymous]
        [HttpGet("IsEmailTaken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> IsEmailTaken([FromQuery] EmailQueryDto query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isTaken = await _authService.EmailExists(query.Email);

            return Ok(isTaken);
        }

        /// <summary>
        ///     Creates a new User Account in the Database
        /// </summary>
        /// <param name="credentials">
        ///     User Credentials that are required for the User to be registered
        /// </param>
        /// <returns>
        ///     No Content
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterDto credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authService.Register(credentials);

            return NoContent();
        }

        /// <summary>
        ///     Validates User Credentials and returns the Users Information alongside a valid Access Token
        /// </summary>
        /// <param name="credentials">
        ///     User Credentials that are required for logging in
        /// </param>
        /// <returns>
        ///     User Information + Access Token
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticatedUser>> Login([FromBody] LoginDto credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (User user, string token) = await _authService.Login(credentials);

            AuthenticatedUser authenticatedUser = _mapper.Map<User, AuthenticatedUser>(user, options =>
            {
                options.Items.Add("Token", token);
            });

            return Ok(authenticatedUser);
        }
    }
}
