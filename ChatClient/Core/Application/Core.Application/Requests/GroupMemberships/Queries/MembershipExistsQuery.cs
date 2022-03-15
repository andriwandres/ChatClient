using Core.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Queries;

public class MembershipExistsQuery : IRequest<bool>
{
    public int GroupMembershipId { get; set; }

    public class Handler : IRequestHandler<MembershipExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MembershipExistsQuery request, CancellationToken cancellationToken = default)
        {
            bool exists = await _unitOfWork.GroupMemberships.Exists(request.GroupMembershipId, cancellationToken);

            return exists;
        }
    }
}