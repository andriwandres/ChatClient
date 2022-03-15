using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Commands;

public class CreateAccountCommand : IRequest<int>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoService _cryptoService;
        private readonly IDateProvider _dateProvider;

        public Handler(ICryptoService cryptoService, IUnitOfWork unitOfWork, IDateProvider dateProvider)
        {
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
            _cryptoService = cryptoService;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken = default)
        {
            User user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
            };

            Recipient recipient = new Recipient { User = user };

            Availability availability = new Availability
            {
                User = user,
                Status = AvailabilityStatus.Offline,
                Modified = _dateProvider.UtcNow(),
                ModifiedManually = false,
            };

            // Generate salt + password hash
            byte[] salt = _cryptoService.GenerateSalt();
            byte[] hash = _cryptoService.HashPassword(request.Password, salt);

            user.PasswordSalt = salt;
            user.PasswordHash = hash;
            user.Created = _dateProvider.UtcNow();

            // Add entites
            await _unitOfWork.Users.Add(user, cancellationToken);
            await _unitOfWork.Recipients.Add(recipient, cancellationToken);
            await _unitOfWork.Availabilities.Add(availability, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return user.UserId;
        }
    }
}