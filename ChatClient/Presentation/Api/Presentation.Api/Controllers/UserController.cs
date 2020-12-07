using AutoMapper;
using Core.Application.Requests.Friendships.Queries;
using Core.Application.Requests.Users.Commands;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Users;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Friendships;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Friendships;
using Presentation.Api.Examples.Users;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Resources.Recipients;
using Presentation.Api.Examples.Recipients;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages user-related data")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new user account
        /// </summary>
        ///
        /// <remarks>
        /// Validates given user credentials and creates a new user account
        /// </remarks>
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
        /// Provided user credentials are in an invalid format
        /// </response>
        ///
        /// <response code="403">
        /// There is already a user with provided userName or email. The user is not allowed to sign up with provided credentials
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred on the server
        /// </response>
        [HttpPost]
        [AllowAnonymous]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerRequestExample(typeof(CreateAccountBody), typeof(CreateAccountBodyExample))]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateAccountBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(CreateAccountForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountBody credentials, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            UserNameOrEmailExistsQuery existsQuery = _mapper.Map<CreateAccountBody, UserNameOrEmailExistsQuery>(credentials);

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (exists)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "A user with the same user name or email already exists. Please use different credentials for creating an account"
                });
            }

            CreateAccountCommand registerCommand = _mapper.Map<CreateAccountBody, CreateAccountCommand>(credentials);

            int userId = await _mediator.Send(registerCommand, cancellationToken);

            return CreatedAtAction(nameof(GetUserProfile), new { userId }, null);
        }

        /// <summary>
        /// Gets a users profile information
        /// </summary>
        ///
        /// <remarks>
        /// Returns profile information of the user with given id
        /// </remarks>
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetUserProfileOkExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetUserProfileNotFoundExample))]
        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<UserProfileResource>> GetUserProfile([FromRoute] int userId, CancellationToken cancellationToken = default)
        {
            GetUserProfileQuery query = new GetUserProfileQuery
            {
                UserId = userId
            };

            UserProfileResource userProfile = await _mediator.Send(query, cancellationToken);

            if (userProfile == null)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"A user with the ID '{userId}' does not exist"
                });
            }

            return Ok(userProfile);
        }

        /// <summary>
        /// Checks email availability
        /// </summary>
        ///
        /// <remarks>
        /// Checks whether or not a given email address already exists in the database.
        /// A successful response (200 OK) means that the email address already exists, whereas an unsuccessful
        /// response (404 Not Found) means that the email address is available and free to use in this system
        /// </remarks>
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
        public async Task<ActionResult> EmailExists([FromQuery] EmailExistsQueryParams model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmailExistsQuery query = _mapper.Map<EmailExistsQueryParams, EmailExistsQuery>(model);

            bool exists = await _mediator.Send(query, cancellationToken);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Checks user name availability
        /// </summary>
        ///
        /// <remarks>
        /// Checks whether or not a given user name already exists in the database.
        /// A successful response (200 OK) means that the user name already exists, whereas an unsuccessful
        /// response (404 Not Found) means that the user name is available and free to use in this system
        /// </remarks>
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
        [HttpHead("name")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UserNameExists([FromQuery] UserNameExistsQueryParams model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserNameExistsQuery query = _mapper.Map<UserNameExistsQueryParams, UserNameExistsQuery>(model);

            bool exists = await _mediator.Send(query, cancellationToken);

            if (exists)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Authenticates the current user
        /// </summary>
        ///
        /// <remarks>
        /// Authenticates the current user by his access token in the Authorization header and returns user information accordingly
        /// </remarks>
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
        /// <response code="500">
        /// An unexpected error occured on the server
        /// </response>
        [HttpGet("me")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AuthenticateOkExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<AuthenticatedUserResource>> Authenticate(CancellationToken cancellationToken = default)
        {
            AuthenticateQuery query = new AuthenticateQuery();

            AuthenticatedUserResource user = await _mediator.Send(query, cancellationToken);

            return Ok(user);
        }

        /// <summary>
        /// Gets all friendships of the current user
        /// </summary>
        ///
        /// <remarks>
        /// Returns all the friendships where the current user is either the requester or the addressee
        /// </remarks>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// List of friendships
        /// </returns>
        ///
        /// <response code="200">
        /// Contains a list of the current users friendships
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("me/friendships")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOwnFriendshipsOkExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<IEnumerable<FriendshipResource>>> GetOwnFriendships(CancellationToken cancellationToken = default)
        {
            GetOwnFriendshipsQuery query = new GetOwnFriendshipsQuery();

            IEnumerable<FriendshipResource> friendships = await _mediator.Send(query, cancellationToken);

            return Ok(friendships);
        }

        /// <summary>
        /// Gets a list of the user's relevant recipients
        /// </summary>
        ///
        /// <remarks>
        /// Returns a list of recipients, where there is a message that is sent to or from the current user. Each recipient contains information about the latest message sent or received to it.
        /// </remarks>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// List of relevant recipients to a users
        /// </returns>
        ///
        /// <response code="200">
        /// Contains list of relevant recipients to the current user
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("me/recipients")]
        [Authorize]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOwnRecipientsOkExample))]
        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<IEnumerable<RecipientResource>>> GetOwnRecipients(CancellationToken cancellationToken = default)
        {
            GetOwnRecipientsQuery query = new GetOwnRecipientsQuery();

            IEnumerable<RecipientResource> relevantRecipients = await _mediator.Send(query, cancellationToken);

            return Ok(relevantRecipients);
        }
    }
}
