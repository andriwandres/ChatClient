using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Dtos.Session;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Session.Queries
{
    public class LoginUserQuery : IRequest<AuthenticatedUser>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthenticatedUser>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICryptoService _cryptoService;

            public LoginUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICryptoService cryptoService)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _cryptoService = cryptoService;
            }

            public async Task<AuthenticatedUser> Handle(LoginUserQuery request, CancellationToken cancellationToken = default)
            {
                User user = await _unitOfWork.Users
                    .GetByUserNameOrEmail(request.UserNameOrEmail)
                    .SingleOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    return null;
                }

                bool passwordCorrect = _cryptoService.VerifyPassword(user.PasswordHash, user.PasswordSalt, request.Password);

                if (!passwordCorrect)
                {
                    return null;
                }

                AuthenticatedUser mappedUser = _mapper.Map<User, AuthenticatedUser>(user);

                mappedUser.Token = _cryptoService.GenerateToken(user);

                return mappedUser;
            }
        }
    }
}
