using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries
{
    public class AuthenticateQuery : IRequest<AuthenticatedUserResource>
    {
        public class Handler : IRequestHandler<AuthenticateQuery, AuthenticatedUserResource>
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

            public async Task<AuthenticatedUserResource> Handle(AuthenticateQuery request, CancellationToken cancellationToken = default)
            {
                int userId = _userProvider.GetCurrentUserId();

                string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                string token = authorizationHeader.Split(' ').Last();

                AuthenticatedUserResource user = await _unitOfWork.Users
                    .GetById(userId)
                    .ProjectTo<AuthenticatedUserResource>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                if (user != null)
                {
                    user.Token = token;
                }

                return user;
            }
        }
    }
}
