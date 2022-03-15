using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Queries;

public class FriendshipCombinationExistsQuery : IRequest<bool>
{
    public int RequesterId { get; set; }
    public int AddresseeId { get; set; }

    public class Handler : IRequestHandler<FriendshipCombinationExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(FriendshipCombinationExistsQuery request, CancellationToken cancellationToken = default)
        {
            bool combinationExists = await _unitOfWork.Friendships.CombinationExists(request.RequesterId, request.AddresseeId, cancellationToken);

            return combinationExists;
        }
    }
}