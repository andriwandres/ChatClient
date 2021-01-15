using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Dtos.Messages;

namespace Core.Application.Requests.Messages.Queries
{
    public class GetMessagesWithRecipientQuery : IRequest<IEnumerable<ChatMessageResource>>
    {
        public int RecipientId { get; set; }
        public int? Limit { get; set; }
        public DateTime? Before { get; set; }
        public DateTime? After { get; set; }

        public class Handler : IRequestHandler<GetMessagesWithRecipientQuery, IEnumerable<ChatMessageResource>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ChatMessageResource>> Handle(GetMessagesWithRecipientQuery request, CancellationToken cancellationToken = default)
            {
                int currentUserId = _userProvider.GetCurrentUserId();

                MessageBoundaries boundaries = _mapper.Map<GetMessagesWithRecipientQuery, MessageBoundaries>(request);

                IEnumerable<ChatMessageResource> messages = await _unitOfWork.MessageRecipients
                    .GetMessagesWithRecipient(currentUserId, request.RecipientId, boundaries)
                    .Select(source => new ChatMessageResource
                    {
                        MessageRecipientId = source.MessageRecipientId,
                        MessageId = source.MessageId,
                        AuthorName = source.Message.Author.UserName,
                        HtmlContent = source.Message.HtmlContent,
                        Created = source.Message.Created,
                        IsOwnMessage = source.Message.AuthorId == currentUserId,
                        IsRead = source.Message.MessageRecipients.All(mr => mr.IsRead),
                    })
                    .ToListAsync(cancellationToken);

                return messages;
            }
        }
    }
}
