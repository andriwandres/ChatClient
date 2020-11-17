using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Commands
{
    public class UpdateFriendshipStatusCommand : IRequest
    {
        public int FriendshipId { get; set; }
        public FriendshipStatusId FriendshipStatusId { get; set; }

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
                FriendshipChange latestMutation = await _unitOfWork.Friendships
                    .GetChanges(request.FriendshipId)
                    .OrderByDescending(mutation => mutation.Created)
                    .FirstAsync(cancellationToken);

                FriendshipChange newChange = new FriendshipChange
                {
                    FriendshipId = request.FriendshipId,
                    StatusId = request.FriendshipStatusId,
                    Created = _dateProvider.UtcNow()
                };

                await _unitOfWork.FriendshipChanges.Add(newChange, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
