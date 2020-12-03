using Core.Application.Database;
using Core.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Queries
{
    public class CanAccessMessageQuery : IRequest<bool>
    {
        public int MessageId { get; set; }

        public class Handler : IRequestHandler<CanAccessMessageQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<bool> Handle(CanAccessMessageQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                bool canAccess = await _unitOfWork.Messages.CanAccess(request.MessageId, userId, cancellationToken);

                return canAccess;
            }
        }
    }
}
