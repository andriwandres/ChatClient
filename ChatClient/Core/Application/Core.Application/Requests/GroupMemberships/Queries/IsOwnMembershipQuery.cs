using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries;

public class IsOwnMembershipQuery : IRequest<bool>
{
    public int GroupMembershipId { get; set; }

    public class Handler : IRequestHandler<IsOwnMembershipQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<bool> Handle(IsOwnMembershipQuery request, CancellationToken cancellationToken = default)
        {
            int currentUserId = _userProvider.GetCurrentUserId();

            GroupMembership membership = await _unitOfWork.GroupMemberships.GetByIdAsync(request.GroupMembershipId);

            return membership.UserId == currentUserId;
        }
    }
}