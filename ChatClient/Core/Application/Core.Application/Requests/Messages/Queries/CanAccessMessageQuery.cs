using Core.Application.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
            private readonly IHttpContextAccessor _httpContextAccessor;

            public async Task<bool> Handle(CanAccessMessageQuery request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                bool canAccess = await _unitOfWork.Messages.CanAccess(request.MessageId, userId, cancellationToken);

                return canAccess;
            }
        }
    }
}
