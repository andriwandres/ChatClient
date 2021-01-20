using AutoMapper;
using Core.Application.Requests.Messages.Commands;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Requests.Recipients.Queries;
using Core.Domain.Dtos.Messages;
using Core.Domain.Resources.Errors;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Examples;
using Presentation.Api.Examples.Messages;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
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

        /// <summary>
        /// Sends a message to one or multiple recipients
        /// </summary>
        ///
        /// <remarks>
        /// Creates a message and sends it to one or multiple users in the system depending on whether the target is a group chat or a private chat
        /// </remarks>
        /// 
        /// <param name="body">
        /// Specifies information about the message to be sent
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Created result with Location header that tells the consumer where to get the created resource
        /// </returns>
        ///
        /// <response code="201">
        /// Sending of message was successful
        /// </response>
        ///
        /// <response code="400">
        /// Model validation of request body has failed
        /// </response>
        ///
        /// <response code="403">
        /// <para>1.) User tried messaging himself</para>
        /// <para>2.) User tried to answer a message from a foreign chat source</para>
        /// </response>
        ///
        /// <response code="404">
        /// <para>1.) Recipient with given ID does not exist</para>
        /// <para>2.) Parent message with given ID does not exist</para>
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPost]
        [Authorize]

        [SwaggerRequestExample(typeof(SendMessageBody), typeof(SendMessageBodyExample))]

        [ProducesResponseType(StatusCodes.Status201Created)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(SendMessageBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(SendMessageForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(SendMessageNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageBody body, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if recipient exists
            RecipientExistsQuery recipientExistsQuery = new RecipientExistsQuery { RecipientId = body.RecipientId };

            bool recipientExists = await _mediator.Send(recipientExistsQuery, cancellationToken);

            if (!recipientExists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Recipient with ID '{body.RecipientId}' does not exist"
                });
            }

            // Check if the the user wants to message himself
            IsOwnRecipientQuery isOwnRecipientQuery = new IsOwnRecipientQuery { RecipientId = body.RecipientId };

            bool isOwnRecipient = await _mediator.Send(isOwnRecipientQuery, cancellationToken);

            if (isOwnRecipient)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You cannot write messages to yourself"
                });
            }

            if (body.ParentId != null)
            {
                // Check if parent message exists
                MessageExistsQuery parentMessageExistsQuery = new MessageExistsQuery { MessageId = body.ParentId.Value };

                bool parentMessageExists = await _mediator.Send(parentMessageExistsQuery, cancellationToken);

                if (!parentMessageExists)
                {
                    return NotFound(new ErrorResource
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Parent message with ID '{body.ParentId}' does not exist"
                    });
                }

                // Check if the user should have access to the parent message
                CanAccessMessageQuery canAccessParentMessageQuery = new CanAccessMessageQuery { MessageId = body.ParentId.Value };

                bool canAccessParentMessage = await _mediator.Send(canAccessParentMessageQuery, cancellationToken);

                if (!canAccessParentMessage)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Message = "You cannot answer messages from a foreign chat"
                    });
                }
            }

            // Send message to their recipients
            SendMessageCommand sendMessageCommand = _mapper.Map<SendMessageBody, SendMessageCommand>(body);

            ChatMessageResource message = await _mediator.Send(sendMessageCommand, cancellationToken);

            return CreatedAtAction(nameof(GetMessageById), new { messageId = message.MessageId }, null);
        }

        /// <summary>
        /// Gets information about a message
        /// </summary>
        ///
        /// <remarks>
        /// Gets detailed information about a single message, given its unique ID.
        /// </remarks>
        /// 
        /// <param name="messageId">
        /// The ID of the message to get
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// Message with given ID
        /// </returns>
        ///
        /// <response code="200">
        /// Contains message with given ID
        /// </response>
        ///
        /// <response code="403">
        /// The user is not permitted to access the given message.
        /// </response>
        ///
        /// <response code="404">
        /// Message with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpGet("{messageId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetMessageByIdOkExample))]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(GetMessageByIdForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetMessageByIdNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult<MessageResource>> GetMessageById([FromRoute] int messageId, CancellationToken cancellationToken = default)
        {
            // Check if the message exists
            MessageExistsQuery existsQuery = new MessageExistsQuery { MessageId = messageId };

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Message with ID '{messageId}' does not exist"
                });
            }

            // Check if the user is allowed to see the message
            CanAccessMessageQuery canAccessQuery = new CanAccessMessageQuery { MessageId = messageId };

            bool canAccess = await _mediator.Send(canAccessQuery, cancellationToken);

            if (!canAccess)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "The user is not permitted to see this message. He may only see messages that he received or wrote himself"
                });
            }

            // Get the message
            GetMessageByIdQuery fetchQuery = new GetMessageByIdQuery { MessageId = messageId };

            MessageResource message = await _mediator.Send(fetchQuery, cancellationToken);

            return Ok(message);
        }

        /// <summary>
        /// Edits a message
        /// </summary>
        ///
        /// <remarks>
        /// Edits the HTML content of a message and marks a message as 'Edited'
        /// </remarks>
        /// 
        /// <param name="messageId">
        /// Unique ID of the message to edit
        /// </param>
        /// 
        /// <param name="body">
        /// Specifies the new HTML content to be applied
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        /// 
        /// <returns>
        /// No content
        /// </returns>
        ///
        /// <response code="204">
        /// Edit was successful
        /// </response>
        ///
        /// <response code="400">
        /// Request body has failed model validation
        /// </response>
        ///
        /// <response code="403">
        /// The user tried to edit a message that is not writtem by himself
        /// </response>
        ///
        /// <response code="404">
        /// A message with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpPut("{messageId:int}")]
        [Authorize]

        [SwaggerRequestExample(typeof(EditMessageBody), typeof(EditMessageBodyExample))]

        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EditMessageBadRequestExample))]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(EditMessageForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(EditMessageNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> EditMessage([FromRoute] int messageId, [FromBody] EditMessageBody body, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the message exists
            MessageExistsQuery existsQuery = new MessageExistsQuery { MessageId = messageId };

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Message with ID '{messageId}' does not exist"
                });
            }

            // Check if the user is the author of the message and thus allowed to update it
            IsAuthorOfMessageQuery isAuthorQuery = new IsAuthorOfMessageQuery { MessageId = messageId };

            bool isAuthor = await _mediator.Send(isAuthorQuery, cancellationToken);

            if (!isAuthor)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Only the author of a message is allowed to update a message"
                });
            }

            // Update the message
            EditMessageCommand editCommand = new EditMessageCommand { MessageId = messageId, HtmlContent = body.HtmlContent };

            await _mediator.Send(editCommand, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Deletes a message
        /// </summary>
        ///
        /// <remarks>
        /// Deletes an existing message with the possibility to re-activate it again later
        /// </remarks>
        /// 
        /// <param name="messageId">
        /// The ID of the message to delete
        /// </param>
        /// 
        /// <param name="cancellationToken">
        /// Notifies asynchronous operations to cancel ongoing work and release resources
        /// </param>
        ///
        /// <returns>
        /// No content
        /// </returns>
        ///
        /// <response code="204">
        /// Deletion was successful
        /// </response>
        ///
        /// <response code="403">
        /// The user is not the author of the message
        /// </response>
        ///
        /// <response code="404">
        /// The message with given ID does not exist
        /// </response>
        ///
        /// <response code="500">
        /// An unexpected error occurred
        /// </response>
        [HttpDelete("{messageId:int}")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(DeleteMessageForbiddenExample))]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(DeleteMessageNotFoundExample))]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResource))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        public async Task<ActionResult> DeleteMessage([FromRoute] int messageId, CancellationToken cancellationToken = default)
        {
            // Check if the message exists
            MessageExistsQuery existsQuery = new MessageExistsQuery { MessageId = messageId };

            bool exists = await _mediator.Send(existsQuery, cancellationToken);

            if (!exists)
            {
                return NotFound(new ErrorResource
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Message with ID '{messageId}' does not exist"
                });
            }

            // Check if the user is the author of the message and thus allowed to update it
            IsAuthorOfMessageQuery isAuthorQuery = new IsAuthorOfMessageQuery { MessageId = messageId };

            bool isAuthor = await _mediator.Send(isAuthorQuery, cancellationToken);

            if (!isAuthor)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResource
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Only the author of a message is allowed to delete a message"
                });
            }

            // Delete the message
            DeleteMessageCommand deleteCommand = new DeleteMessageCommand { MessageId = messageId };

            await _mediator.Send(deleteCommand, cancellationToken);

            return NoContent();
        }
    }
}
