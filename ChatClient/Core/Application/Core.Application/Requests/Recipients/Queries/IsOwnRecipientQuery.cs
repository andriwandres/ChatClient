using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Recipients.Queries
{
    public class IsOwnRecipientQuery : IRequest<bool>
    {
        public int RecipientId { get; set; }

        public class Handler : IRequestHandler<IsOwnRecipientQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<bool> Handle(IsOwnRecipientQuery request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Recipient recipient = await _unitOfWork.Recipients
                    .GetById(request.RecipientId)
                    .SingleOrDefaultAsync(cancellationToken);

                return recipient.UserId == userId;
            }
        }
    }
}
