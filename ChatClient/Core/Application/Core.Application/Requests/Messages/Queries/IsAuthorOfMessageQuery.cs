using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Messages.Queries
{
    public class IsAuthorOfMessageQuery : IRequest<bool>
    {
        public int MessageId { get; set; }

        public class Handler : IRequestHandler<IsAuthorOfMessageQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<bool> Handle(IsAuthorOfMessageQuery request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Message message = await _unitOfWork.Messages
                    .GetById(request.MessageId)
                    .SingleOrDefaultAsync(cancellationToken);

                return message.AuthorId == userId;
            }
        }
    }
}
