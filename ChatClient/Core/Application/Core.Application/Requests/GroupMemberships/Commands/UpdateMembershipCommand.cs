using Core.Application.Database;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.GroupMemberships.Commands
{
    public class UpdateMembershipCommand : IRequest
    {
        public int GroupMembershipId { get; set; }
        public bool IsAdmin { get; set; }

        public class Handler : IRequestHandler<UpdateMembershipCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateMembershipCommand request, CancellationToken cancellationToken = default)
            {
                GroupMembership membership = await _unitOfWork.GroupMemberships.GetByIdAsync(request.GroupMembershipId);

                membership.IsAdmin = request.IsAdmin;

                _unitOfWork.GroupMemberships.Update(membership);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
