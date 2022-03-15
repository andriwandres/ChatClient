using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Availabilities.Commands
{
    public class UpdateAvailabilityCommand : IRequest
    {
        public Domain.Enums.AvailabilityStatus AvailabilityStatus { get; set; }

        public class Handler : IRequestHandler<UpdateAvailabilityCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserProvider _userProvider;
            private readonly IDateProvider _dateProvider;

            public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider, IDateProvider dateProvider)
            {
                _unitOfWork = unitOfWork;
                _userProvider = userProvider;
                _dateProvider = dateProvider;
            }

            public async Task<Unit> Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                Availability availability = await _unitOfWork.Availabilities.GetByUser(userId);

                availability.Status = request.AvailabilityStatus;
                availability.Modified = _dateProvider.UtcNow();
                availability.ModifiedManually = true;

                _unitOfWork.Availabilities.Update(availability);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
