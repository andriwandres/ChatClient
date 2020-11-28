using Core.Application.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<bool> Handle(CanDeleteMembershipQuery request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                bool canDelete = await _unitOfWork.GroupMemberships.CanDeleteMembership(userId, request.GroupMembershipIdToDelete, cancellationToken);

                return canDelete;
            }
        }
    }
}
