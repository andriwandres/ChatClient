using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Queries
{
    public class FriendshipExistsQuery : IRequest<bool>
    {
        public int FriendshipId { get; set; }

        public class Handler : IRequestHandler<FriendshipExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(FriendshipExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.Friendships.Exists(request.FriendshipId, cancellationToken);

                return exists;
            }
        }
    }
}
