using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels.Users;

namespace Core.Application.Requests.Session.Commands;

public class LoginCommand : IRequest<AuthenticatedUserViewModel>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<LoginCommand, AuthenticatedUserViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoService _cryptoService;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper, ICryptoService cryptoService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cryptoService = cryptoService;
        }

        public async Task<AuthenticatedUserViewModel> Handle(LoginCommand request, CancellationToken cancellationToken = default)
        {
            User user = await _unitOfWork.Users.GetByUserNameOrEmail(request.UserNameOrEmail);

            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = _cryptoService.VerifyPassword(user.PasswordHash, user.PasswordSalt, request.Password);

            if (!passwordCorrect)
            {
                return null;
            }

            AuthenticatedUserViewModel mappedUser = _mapper.Map<User, AuthenticatedUserViewModel>(user);

            mappedUser.Token = _cryptoService.GenerateToken(user);

            return mappedUser;
        }
    }
}