using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Recipients.Queries
{
    public class IsOwnRecipientQuery : IRequest<bool>
    {
        public int RecipientId { get; set; }

        public class Handler : IRequestHandler<IsOwnRecipientQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<bool> Handle(IsOwnRecipientQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                Recipient recipient = await _unitOfWork.Recipients.GetByIdAsync(request.RecipientId);

                return recipient.UserId == userId;
            }
        }
    }
}
