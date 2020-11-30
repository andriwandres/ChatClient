using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manages chat messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MessageController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SendMessage(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }

        [HttpGet("{messageId:int}")]
        [Authorize]
        public async Task<ActionResult> GetMessageById(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }

        [HttpPut("{messageId:int}")]
        [Authorize]
        public async Task<ActionResult> EditMessage(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }

        [HttpDelete("{messageId:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteMessage(CancellationToken cancellationToken = default)
        {
            return NoContent();
        }
    }
}
