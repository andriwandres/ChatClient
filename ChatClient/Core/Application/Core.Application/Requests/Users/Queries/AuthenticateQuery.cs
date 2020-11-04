using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries
{
    public class AuthenticateQuery : IRequest<AuthenticatedUser>
    {
        public class AuthenticateQueryHandler : IRequestHandler<AuthenticateQuery, AuthenticatedUser>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public AuthenticateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<AuthenticatedUser> Handle(AuthenticateQuery request, CancellationToken cancellationToken = default)
            {
                string id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                string token = authorizationHeader.Split(' ').Last();

                AuthenticatedUser user = await _unitOfWork.Users
                    .GetById(int.Parse(id))
                    .ProjectTo<AuthenticatedUser>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    return null;
                }

                user.Token = token;

                return user;
            }
        }
    }
}
