using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Queries;

public class GetFriendshipByIdQuery : IRequest<FriendshipResource>
{
    public int FriendshipId { get; set; }

    public class Handler : IRequestHandler<GetFriendshipByIdQuery, FriendshipResource>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FriendshipResource> Handle(GetFriendshipByIdQuery request, CancellationToken cancellationToken = default)
        {
            Friendship friendship = await _unitOfWork.Friendships.GetByIdAsync(request.FriendshipId);

            return _mapper.Map<Friendship, FriendshipResource>(friendship);
        }
    }
}