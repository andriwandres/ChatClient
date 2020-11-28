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
    public class CanCreateMembershipQuery : IRequest<bool>
    {
        public int GroupId { get; set; }

        public class Handler : IRequestHandler<CanCreateMembershipQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<bool> Handle(CanCreateMembershipQuery request, CancellationToken cancellationToken = default)
            {
                // Get current user id
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Get the users membership in this group
                GroupMembership membership = await _unitOfWork.GroupMemberships
                    .GetByCombination(request.GroupId, userId)
                    .SingleOrDefaultAsync(cancellationToken);

                return membership != null && membership.IsAdmin;
            }
        }
    }
}
