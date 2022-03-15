using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Commands;

public class UpdateFriendshipStatusCommand : IRequest
{
    public int FriendshipId { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; }

    public class Handler : IRequestHandler<UpdateFriendshipStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateProvider _dateProvider;

        public Handler(IUnitOfWork unitOfWork, IDateProvider dateProvider)
        {
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
        }

        public async Task<Unit> Handle(UpdateFriendshipStatusCommand request, CancellationToken cancellationToken = default)
        {
            FriendshipChange newChange = new FriendshipChange
            {
                FriendshipId = request.FriendshipId,
                Status = request.FriendshipStatus,
                Created = _dateProvider.UtcNow()
            };

            await _unitOfWork.FriendshipChanges.Add(newChange, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}