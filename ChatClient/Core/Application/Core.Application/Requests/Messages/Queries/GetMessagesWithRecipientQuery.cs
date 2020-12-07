using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Resources.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Queries
{
    public class GetMessagesWithRecipientQuery : IRequest<IEnumerable<ChatMessageResource>>
    {
        public int RecipientId { get; set; }

        public class Handler : IRequestHandler<GetMessagesWithRecipientQuery, IEnumerable<ChatMessageResource>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<IEnumerable<ChatMessageResource>> Handle(GetMessagesWithRecipientQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                IEnumerable<ChatMessageResource> messages = await _unitOfWork.MessageRecipients
                    .GetMessagesWithRecipient(userId, request.RecipientId)
                    .ProjectTo<ChatMessageResource>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return messages;
            }
        }
    }
}
