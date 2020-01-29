using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.Auth;
using ChatClient.Core.Models.ViewModels.Auth;
using ChatClient.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthenticatedUser>> Authenticate()
        {
            AuthenticatedUser user = await _authService.Authenticate();

            return Ok(user);
        }

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

            bool isTaken = await _authService.IsEmailTaken(query.Email);

            return Ok(isTaken);
        }

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

            AuthenticatedUser user = await _authService.Login(credentials);

            return Ok(user);
        }

        [HttpGet("Test")]
        [Authorize]
        public async Task<ActionResult> Test()
        {
            User user = await _authService.GetUser();

            string pw = Convert.ToBase64String(user.PasswordSalt);

            return Ok(pw);
        }
    }
}
