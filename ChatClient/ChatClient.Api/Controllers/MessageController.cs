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
        private readonly IAuthService _authService;
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService, IAuthService authService, IMapper mapper)
        {
            _mapper = mapper;
            _authService = authService;
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet("GetLatestMessages")]
        public async Task<ActionResult<IEnumerable<LatestMessage>>> GetLatestMessages()
        {
            IEnumerable<MessageRecipient> latestMessages = await _messageService.GetLatestMessages();
            IEnumerable<LatestMessage> viewModels = _mapper.Map<IEnumerable<LatestMessage>>(latestMessages);

            return Ok(viewModels);
        }

        [Authorize]
        [HttpGet("GetGroupMessages/{groupId:int}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetGroupMessages([FromRoute] int groupId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _authService.GetUser();

            IEnumerable<MessageRecipient> recipients = await _messageService.GetGroupMessages(user.UserId, groupId);

            IEnumerable<ChatMessage> messages = _mapper.Map<IEnumerable<ChatMessage>>(recipients, options =>
            {
                options.Items["UserId"] = user.UserId;
            });

            return Ok(messages);
        }

        [Authorize]
        [HttpGet("GetPrivateMessages/{recipientId:int}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetPrivateMessages([FromRoute] int recipientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _authService.GetUser();

            IEnumerable<MessageRecipient> recipients = await _messageService.GetPrivateMessages(user.UserId, recipientId);

            IEnumerable<ChatMessage> messages = _mapper.Map<IEnumerable<ChatMessage>>(recipients, options =>
            {
                options.Items["UserId"] = user.UserId;
            });

            return Ok(messages);
        }
    }
}
