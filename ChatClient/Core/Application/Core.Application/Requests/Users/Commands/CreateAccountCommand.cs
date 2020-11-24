using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Commands
{
    public class CreateAccountCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<CreateAccountCommand, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICryptoService _cryptoService;
            private readonly IDateProvider _dateProvider;

            public RegisterUserCommandHandler(ICryptoService cryptoService, IUnitOfWork unitOfWork, IDateProvider dateProvider)
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
                    UserName = request.UserName
                };

                // Generate salt + password hash
                byte[] salt = _cryptoService.GenerateSalt();
                byte[] hash = _cryptoService.HashPassword(request.Password, salt);

                user.PasswordSalt = salt;
                user.PasswordHash = hash;
                user.Created = _dateProvider.UtcNow();

                await _unitOfWork.Users.Add(user, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return user.UserId;
            }
        }
    }
}
