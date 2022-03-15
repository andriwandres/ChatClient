using Core.Application.Database;
using Core.Application.Hubs;
using Core.Application.Services;
using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Commands
{
    public class SendMessageCommand : IRequest<ChatMessageResource>
    {
        public int RecipientId { get; set; }
        public int? ParentId { get; set; }
        public string HtmlContent { get; set; }

        public class Handler : IRequestHandler<SendMessageCommand, ChatMessageResource>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDateProvider _dateProvider;
            private readonly IUserProvider _userProvider;
            private readonly IHubContext<ChatHubBase, IHubClient> _hubContext;

            public Handler(IUnitOfWork unitOfWork, IDateProvider dateProvider, IUserProvider userProvider, IHubContext<ChatHubBase, IHubClient> hubContext)
            {
                _unitOfWork = unitOfWork;
                _dateProvider = dateProvider;
                _userProvider = userProvider;
                _hubContext = hubContext;
            }

            public async Task<ChatMessageResource> Handle(SendMessageCommand request, CancellationToken cancellationToken = default)
            {
                int currentUserId = _userProvider.GetCurrentUserId();

                MessageRecipient ownMessageRecipient;

                Func<Task> notificationFactory;

                // Message to be stored
                Message message = new Message
                {
                    AuthorId = currentUserId,
                    ParentId = request.ParentId,
                    HtmlContent = request.HtmlContent,
                    Created = _dateProvider.UtcNow(),
                    IsEdited = false,
                };

                await _unitOfWork.Messages.Add(message, cancellationToken);

                // Recipient to send the message to
                Recipient recipient = await _unitOfWork.Recipients.GetByIdIncludingMemberships(request.RecipientId, cancellationToken);

                // Send group chat message to all members of the group
                if (recipient.UserId == null)
                {
                    IEnumerable<GroupMembership> members = recipient.GroupMembership.Group.Memberships;

                    IEnumerable<MessageRecipient> messageRecipients = members.Select(member => new MessageRecipient
                    {
                        Message = message,
                        MessageId = message.MessageId,
                        RecipientId = member.Recipient.RecipientId,
                        Recipient = member.Recipient,
                        ReadDate = null,
                        IsForwarded = false,
                        IsRead = member.UserId == currentUserId,
                    });

                    // Get instance of relevant MessageRecipient for the current user
                    ownMessageRecipient = messageRecipients
                        .Single(mr => mr.Recipient.GroupMembership.UserId == currentUserId);

                    await _unitOfWork.MessageRecipients.AddRange(messageRecipients, cancellationToken);

                    // Pass task to perform when notifying users
                    notificationFactory = async () => await NotifyGroupChatRecipients(members, messageRecipients);
                }

                // Send private chat message
                else
                {
                    MessageRecipient messageRecipient = new()
                    {
                        Message = message,
                        MessageId = message.MessageId,
                        RecipientId = recipient.RecipientId,
                        Recipient = recipient,
                        ReadDate = null,
                        IsForwarded = false,
                        IsRead = false
                    };

                    ownMessageRecipient = messageRecipient;

                    await _unitOfWork.MessageRecipients.Add(messageRecipient, cancellationToken);

                    // Pass task to perform when notifying the recipient user
                    notificationFactory = async () => await NotifyPrivateChatRecipient(messageRecipient);
                }
                
                // Commit changes to the database
                await _unitOfWork.CommitAsync(cancellationToken);

                // Notify recipient(s) of message
                await notificationFactory();

                return new ChatMessageResource
                {
                    MessageRecipientId = ownMessageRecipient.MessageRecipientId,
                    MessageId = ownMessageRecipient.MessageId,
                    HtmlContent = ownMessageRecipient.Message.HtmlContent,
                    AuthorName = ownMessageRecipient.Message.HtmlContent,
                    Created = ownMessageRecipient.Message.Created,
                    IsOwnMessage = true,
                    IsRead = true,
                };
            }

            /// <summary>
            /// Notifies all group chat members that a new message has been sent
            /// </summary>
            /// <param name="members">List of members to notify</param>
            /// <param name="messageRecipients">List of MessageRecipients that have been added</param>
            /// <returns>Asynchronous Task</returns>
            private async Task NotifyGroupChatRecipients(IEnumerable<GroupMembership> members, IEnumerable<MessageRecipient> messageRecipients)
            {
                int currentUserId = _userProvider.GetCurrentUserId();

                User author = await _unitOfWork.Users.GetByIdAsync(currentUserId);

                // Get a list of all users to notify (except the author)
                IEnumerable<int> userIds = members
                    .Select(member => member.UserId)
                    .Except(new[] {currentUserId});

                foreach (int userId in userIds)
                {
                    // Get instance of relevant MessageRecipient for the user to notify
                    MessageRecipient relevantMessageRecipient = messageRecipients
                        .Single(mr => mr.Recipient.GroupMembership.UserId == userId);

                    // Map message to a view model
                    ChatMessageResource message = new ChatMessageResource
                    {
                        MessageRecipientId = relevantMessageRecipient.MessageRecipientId,
                        MessageId = relevantMessageRecipient.MessageId,
                        AuthorName = author.UserName,
                        HtmlContent = relevantMessageRecipient.Message.HtmlContent,
                        Created = relevantMessageRecipient.Message.Created,
                        IsOwnMessage = false,
                        IsRead = false
                    };

                    // Notify user
                    await _hubContext.Clients.User(userId.ToString()).ReceiveMessage(new ReceiveMessagePayload
                    {
                        RecipientId = relevantMessageRecipient.RecipientId,
                        Message = message,
                    });
                }
            }

            /// <summary>
            /// Notifies the recipient of a message after a private message has been sent
            /// </summary>
            /// <param name="messageRecipient">MessageRecipient that has been added</param>
            /// <returns>Asynchronous task</returns>
            private async Task NotifyPrivateChatRecipient(MessageRecipient messageRecipient)
            {
                int currentUserId = _userProvider.GetCurrentUserId();

                User author = await _unitOfWork.Users.GetByIdAsync(currentUserId);

                // Map message to a view model
                ChatMessageResource message = new()
                {
                    MessageRecipientId = messageRecipient.MessageRecipientId,
                    MessageId = messageRecipient.MessageId,
                    AuthorName = author.UserName,
                    HtmlContent = messageRecipient.Message.HtmlContent,
                    Created = messageRecipient.Message.Created,
                    IsOwnMessage = false,
                    IsRead = false,
                };

                // Notify user
                await _hubContext.Clients
                    .User(messageRecipient.Recipient.UserId.ToString())
                    .ReceiveMessage(new ReceiveMessagePayload
                    {
                        RecipientId = author.Recipient.RecipientId,
                        Message = message
                    });
            }
        }
    }
}
