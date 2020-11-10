using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/friendships")]
    [Produces("application/json")]
    [SwaggerTag("Manages friendships between users")]
    public class FriendshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RequestFriendship()
        {
            return NoContent();
        }

        [HttpPut("{friendshipId:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateFriendshipStatus()
        {
            return NoContent();
        }

        [HttpDelete("{friendshipId:int}")]
        [Authorize]
        public async Task<ActionResult> RemoveFriendship()
        {
            return NoContent();
        }
    }
}
