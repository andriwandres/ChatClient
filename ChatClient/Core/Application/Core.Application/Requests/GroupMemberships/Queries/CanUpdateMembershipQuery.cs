using Core.Application.Database;
using Core.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries
{
    public class CanUpdateMembershipQuery : IRequest<bool>
    {
        public int GroupMembershipIdToUpdate { get; set; }

        public class Handler : IRequestHandler<CanUpdateMembershipQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<bool> Handle(CanUpdateMembershipQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                bool canUpdate = await _unitOfWork.GroupMemberships.CanUpdateMembership(userId, request.GroupMembershipIdToUpdate, cancellationToken);

                return canUpdate;
            }
        }
    }
}
