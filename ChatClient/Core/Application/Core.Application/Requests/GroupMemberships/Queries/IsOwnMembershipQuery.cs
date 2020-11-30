using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries
{
    public class IsOwnMembershipQuery : IRequest<bool>
    {
        public int GroupMembershipId { get; set; }

        public class Handler : IRequestHandler<IsOwnMembershipQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<bool> Handle(IsOwnMembershipQuery request, CancellationToken cancellationToken = default)
            {
                int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                GroupMembership membership = await _unitOfWork.GroupMemberships
                    .GetById(request.GroupMembershipId)
                    .SingleOrDefaultAsync(cancellationToken);

                return membership.UserId == currentUserId;
            }
        }
    }
}
