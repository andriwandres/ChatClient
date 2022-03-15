using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries;

public class CanCreateMembershipQuery : IRequest<bool>
{
    public int GroupId { get; set; }

    public class Handler : IRequestHandler<CanCreateMembershipQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _userProvider = userProvider;
        }

        public async Task<bool> Handle(CanCreateMembershipQuery request, CancellationToken cancellationToken = default)
        {
            // Get current user id
            int userId = _userProvider.GetCurrentUserId();

            // Get the users membership in this group
            GroupMembership membership = await _unitOfWork.GroupMemberships.GetByCombination(request.GroupId, userId, cancellationToken);

            return membership != null && membership.IsAdmin;
        }
    }
}