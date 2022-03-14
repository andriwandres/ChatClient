using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
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
            private readonly IUserProvider _userProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
            }

            public async Task<bool> Handle(IsAuthorOfMessageQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                Message message = await _unitOfWork.Messages.GetByIdAsync(request.MessageId);

                return message.AuthorId == userId;
            }
        }
    }
}
