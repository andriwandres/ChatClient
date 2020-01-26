using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using ChatClient.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _mapper = mapper;
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet("GetLatestMessages")]
        public async Task<ActionResult> GetLatestMessages()
        {
            IEnumerable<MessageRecipient> latestMessages = await _messageService.GetLatestMessages();
            IEnumerable<LatestMessage> viewModels = _mapper.Map<IEnumerable<LatestMessage>>(latestMessages);

            return Ok(viewModels);
        }
    }
}
