using Core.Application.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<bool> Handle(CanUpdateMembershipQuery request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                bool canUpdate = await _unitOfWork.GroupMemberships.CanUpdateMembership(userId, request.GroupMembershipIdToUpdate, cancellationToken);

                return canUpdate;
            }
        }
    }
}
