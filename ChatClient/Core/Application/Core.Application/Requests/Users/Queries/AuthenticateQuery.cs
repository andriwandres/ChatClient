using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries;

public class AuthenticateQuery : IRequest<AuthenticatedUserViewModel>
{
    public class Handler : IRequestHandler<AuthenticateQuery, AuthenticatedUserViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserProvider userProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userProvider = userProvider;
        }

        public async Task<AuthenticatedUserViewModel> Handle(AuthenticateQuery request, CancellationToken cancellationToken = default)
        {
            int userId = _userProvider.GetCurrentUserId();

            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            string token = authorizationHeader.Split(' ').Last();

            User user = await _unitOfWork.Users.GetByIdAsync(userId);
                
            AuthenticatedUserViewModel authenticatedUserViewModel = _mapper.Map<User, AuthenticatedUserViewModel>(user);

            if (authenticatedUserViewModel != null)
            {
                authenticatedUserViewModel.Token = token;
            }

            return authenticatedUserViewModel;
        }
    }
}