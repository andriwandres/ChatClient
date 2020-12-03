using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Commands
{
    public class SendMessageCommand : IRequest<int>
    {
        public int RecipientId { get; set; }
        public int? ParentId { get; set; }
        public string HtmlContent { get; set; }

        public class Handler : IRequestHandler<SendMessageCommand, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDateProvider _dateProvider;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IDateProvider dateProvider, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _dateProvider = dateProvider;
                _userProvider = userProvider;
            }

            public async Task<int> Handle(SendMessageCommand request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                Message message = new Message
                {
                    AuthorId = userId,
                    ParentId = request.ParentId,
                    HtmlContent = request.HtmlContent,
                    Created = _dateProvider.UtcNow(),
                    IsEdited = false,
                };

                await _unitOfWork.Messages.Add(message, cancellationToken);

                Recipient recipient = await _unitOfWork.Recipients
                    .GetById(request.RecipientId)
                    .AsTracking()
                    .Include(r => r.GroupMembership)
                    .ThenInclude(gm => gm.Group)
                    .ThenInclude(g => g.Memberships)
                    .ThenInclude(gm => gm.Recipient)
                    .SingleOrDefaultAsync(cancellationToken);

                // Send group chat message to all members of the group
                if (recipient.UserId == null)
                {
                    IEnumerable<GroupMembership> members = recipient.GroupMembership.Group.Memberships;

                    IEnumerable<MessageRecipient> messageRecipients = members.Select(member => new MessageRecipient
                    {
                        Message = message,
                        RecipientId = member.Recipient.RecipientId,
                        ReadDate = null,
                        IsForwarded = false,
                        IsRead = member.UserId == userId,
                    });

                    await _unitOfWork.MessageRecipients.AddRange(messageRecipients, cancellationToken);
                }

                // Send private chat message
                else
                {
                    MessageRecipient messageRecipient = new MessageRecipient
                    {
                        Message = message,
                        RecipientId = request.RecipientId,
                        ReadDate = null,
                        IsForwarded = false,
                        IsRead = recipient.UserId == userId
                    };

                    await _unitOfWork.MessageRecipients.Add(messageRecipient, cancellationToken);
                }
                
                await _unitOfWork.CommitAsync(cancellationToken);

                return message.MessageId;
            }
        }
    }
}
