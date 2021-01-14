using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Queries
{
    public class GetMessagesWithRecipientQuery : IRequest<IEnumerable<ChatMessageResource>>
    {
        public int RecipientId { get; set; }

        public class Handler : IRequestHandler<GetMessagesWithRecipientQuery, IEnumerable<ChatMessageResource>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<IEnumerable<ChatMessageResource>> Handle(GetMessagesWithRecipientQuery request, CancellationToken cancellationToken = default)
            {
                int currentUserId = _userProvider.GetCurrentUserId();

                IEnumerable<ChatMessageResource> messages = await _unitOfWork.MessageRecipients
                    .GetMessagesWithRecipient(currentUserId, request.RecipientId)
                    .Select(source => new ChatMessageResource
                    {
                        MessageRecipientId = source.MessageRecipientId,
                        MessageId = source.MessageId,
                        AuthorName = source.Message.Author.UserName,
                        HtmlContent = source.Message.HtmlContent,
                        Created = source.Message.Created,
                        IsOwnMessage = source.Message.AuthorId == currentUserId,
                        IsRead = source.Message.MessageRecipients.All(mr => mr.IsRead || mr.Message.AuthorId == currentUserId),
                    })
                    .ToListAsync(cancellationToken);

                return messages;
            }
        }
    }
}
