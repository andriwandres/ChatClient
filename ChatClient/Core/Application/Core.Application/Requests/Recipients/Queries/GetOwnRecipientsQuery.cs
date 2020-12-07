using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Resources.Recipients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Requests.Recipients.Queries
{
    public class GetOwnRecipientsQuery : IRequest<IEnumerable<RecipientResource>>
    {
        public class Handler : IRequestHandler<GetOwnRecipientsQuery, IEnumerable<RecipientResource>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
                _mapper = mapper;
            }

            public async Task<IEnumerable<RecipientResource>> Handle(GetOwnRecipientsQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                // Fetch relevant recipients for the current user alongside the latest message with each recipient
                IEnumerable<RecipientResource> recipients = await _unitOfWork.MessageRecipients
                    .GetLatestGroupedByRecipients(userId)
                    .ProjectTo<RecipientResource>(_mapper.ConfigurationProvider, new { userId })
                    .ToListAsync(cancellationToken);

                return recipients;
            }
        }
    }
}