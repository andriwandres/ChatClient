using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries
{
    public class MembershipCombinationExistsQuery : IRequest<bool>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public class Handler : IRequestHandler<MembershipCombinationExistsQuery, bool>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(MembershipCombinationExistsQuery request, CancellationToken cancellationToken = default)
            {
                bool exists = await _unitOfWork.GroupMemberships.CombinationExists(request.GroupId, request.UserId, cancellationToken);

                return exists;
            }
        }
    }
}
