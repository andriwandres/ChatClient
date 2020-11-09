using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Database;
using Core.Domain.Resources.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileResource>
    {
        public int UserId { get; set; }

        public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public GetUserProfileQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<UserProfileResource> Handle(GetUserProfileQuery request, CancellationToken cancellationToken = default)
            {
                UserProfileResource userProfile = await _unitOfWork.Users
                    .GetById(request.UserId)
                    .ProjectTo<UserProfileResource>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                return userProfile;
            }
        }
    }
}
