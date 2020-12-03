using Core.Application.Database;
using Core.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries
{
    public class CanDeleteMembershipQuery : IRequest<bool>
    {
        public int GroupMembershipIdToDelete { get; set; }

        public class Handler : IRequestHandler<CanDeleteMembershipQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<bool> Handle(CanDeleteMembershipQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                bool canDelete = await _unitOfWork.GroupMemberships.CanDeleteMembership(userId, request.GroupMembershipIdToDelete, cancellationToken);

                return canDelete;
            }
        }
    }
}
