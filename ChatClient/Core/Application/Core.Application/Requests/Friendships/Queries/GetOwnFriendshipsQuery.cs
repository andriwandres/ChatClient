using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources.Friendships;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Queries
{
    public class GetOwnFriendshipsQuery : IRequest<IEnumerable<FriendshipResource>>
    {
        public class GetOwnFriendshipsQueryHandler : IRequestHandler<GetOwnFriendshipsQuery, IEnumerable<FriendshipResource>>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetOwnFriendshipsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<IEnumerable<FriendshipResource>> Handle(GetOwnFriendshipsQuery request, CancellationToken cancellationToken = default)
            {
                // Get the current users id
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                IEnumerable<FriendshipResource> friendships = await _unitOfWork.Friendships
                    .GetByUser(userId)
                    .ProjectTo<FriendshipResource>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return friendships;
            }
        }
    }
}
