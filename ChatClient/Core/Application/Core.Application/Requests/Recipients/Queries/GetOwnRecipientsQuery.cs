using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using Core.Domain.Resources.Messages;
using Core.Domain.Resources.Recipients;
using Core.Domain.Resources.Users;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Recipients.Queries
{
    public class GetOwnRecipientsQuery : IRequest<IEnumerable<RecipientResource>>
    {
        public class Handler : IRequestHandler<GetOwnRecipientsQuery, IEnumerable<RecipientResource>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<IEnumerable<RecipientResource>> Handle(GetOwnRecipientsQuery request, CancellationToken cancellationToken = default)
            {
                int currentUserId = _userProvider.GetCurrentUserId();

                // Fetch relevant recipients for the current user alongside the latest message with each recipient
                List<MessageRecipient> latestGroupedByRecipients = await _unitOfWork.MessageRecipients
                    .GetLatestGroupedByRecipients(currentUserId);

                IEnumerable<RecipientResource> recipients = latestGroupedByRecipients
                    .Select(source => new RecipientResource
                    {
                        RecipientId = source.Recipient.UserId != currentUserId
                            ? source.RecipientId
                            : source.Message.Author.Recipient.RecipientId,

                        TargetGroup = source.Recipient.GroupMembershipId == null
                            ? null
                            : new TargetGroupResource
                            {
                                GroupId = source.Recipient.GroupMembership.GroupId,
                                Name = source.Recipient.GroupMembership.Group.Name,
                            },

                        TargetUser = source.Recipient.UserId == null
                            ? null
                            : new TargetUserResource
                            {
                                UserId = source.Recipient.UserId == currentUserId
                                    ? source.Message.AuthorId
                                    : source.Recipient.User.UserId,
                                UserName = source.Recipient.UserId == currentUserId
                                    ? source.Message.Author.UserName
                                    : source.Recipient.User.UserName
                            },

                        LatestMessage = new LatestMessageResource
                        {
                            MessageId = source.MessageId,
                            MessageRecipientId = source.MessageRecipientId,
                            AuthorId = source.Message.AuthorId,
                            AuthorName = source.Message.Author.UserName,
                            HtmlContent = source.Message.HtmlContent,
                            IsRead = source.IsRead,
                            Created = source.Message.Created,
                            IsOwnMessage = source.Message.AuthorId == currentUserId
                        },

                        AvailabilityStatus = source.Recipient.UserId == null
                            ? 0
                            : source.Recipient.UserId == currentUserId
                                ? source.Message.Author.Availability.Status
                                : source.Recipient.User.Availability.Status,

                        IsPinned = source.Recipient.UserId == currentUserId
                            ? source.Message.Author.Recipient.Pins.Any(pin => pin.UserId == currentUserId)
                            : source.Recipient.Pins.Any(pin => pin.UserId == currentUserId),

                        UnreadMessagesCount = source.Recipient.UserId == null
                            ? source.Recipient.ReceivedMessages.Count(mr => mr.IsRead == false && mr.Message.AuthorId != currentUserId)

                            : source.Recipient.UserId != currentUserId
                                ? 0
                                : source.Recipient.ReceivedMessages.Count(mr => mr.IsRead == false && mr.Message.AuthorId == source.Message.AuthorId)
                    });

                return recipients;
            }
        }
    }
}
