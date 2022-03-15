using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Users.Queries;

public class GetUserProfileQuery : IRequest<UserProfileResource>
{
    public int UserId { get; set; }

    public class Handler : IRequestHandler<GetUserProfileQuery, UserProfileResource>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileResource> Handle(GetUserProfileQuery request, CancellationToken cancellationToken = default)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(request.UserId);

            return _mapper.Map<User, UserProfileResource>(user);
        }
    }
}