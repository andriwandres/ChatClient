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

        /// <summary>
        ///     Gets the latest messages for a user grouped by private chats and group chats
        /// </summary>
        /// <returns>
        ///     Latest messages of a user
        /// </returns>
        [Authorize]
        [HttpGet("GetLatestMessages")]
        public async Task<ActionResult<IEnumerable<LatestMessageViewModel>>> GetLatestMessages()
        {
            IEnumerable<MessageRecipient> recipients = await _messageService.GetLatestMessages();

            IEnumerable<LatestMessageViewModel> latestMessages = _mapper.Map<IEnumerable<LatestMessageViewModel>>(recipients);

            return Ok(latestMessages);
        }

        /// <summary>
        ///     Gets all messages of a specific group chat
        /// </summary>
        /// <param name="groupId">
        ///     Group ID of group chat to load messages from
        /// </param>
        /// <returns>
        ///     Messages of specific group chat
        /// </returns>
        [Authorize]
        [HttpGet("GetGroupMessages/{groupId:int}")]
        public async Task<ActionResult<IEnumerable<ChatMessageViewModel>>> GetGroupMessages([FromRoute] int groupId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _authService.GetUser();

            IEnumerable<MessageRecipient> recipients = await _messageService.GetGroupMessages(user.UserId, groupId);

            IEnumerable<ChatMessageViewModel> messages = _mapper.Map<IEnumerable<ChatMessageViewModel>>(recipients, options =>
            {
                options.Items["UserId"] = user.UserId;
            });

            return Ok(messages);
        }

        /// <summary>
        ///     Gets all messages of a specific private chat with a user
        /// </summary>
        /// <param name="recipientId">
        ///     User ID of private chat to load messages from
        /// </param>
        /// <returns>
        ///     Messages of specific private chat
        /// </returns>
        [Authorize]
        [HttpGet("GetPrivateMessages/{recipientId:int}")]
        public async Task<ActionResult<IEnumerable<ChatMessageViewModel>>> GetPrivateMessages([FromRoute] int recipientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _authService.GetUser();

            IEnumerable<MessageRecipient> recipients = await _messageService.GetPrivateMessages(user.UserId, recipientId);

            IEnumerable<ChatMessageViewModel> messages = _mapper.Map<IEnumerable<ChatMessageViewModel>>(recipients, options =>
            {
                options.Items["UserId"] = user.UserId;
            });

            return Ok(messages);
        }
    }
}
