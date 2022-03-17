using AutoMapper;
using Core.Application.Database;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Queries;

public class GetFriendshipByIdQuery : IRequest<FriendshipViewModel>
{
    public int FriendshipId { get; set; }

    public class Handler : IRequestHandler<GetFriendshipByIdQuery, FriendshipViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FriendshipViewModel> Handle(GetFriendshipByIdQuery request, CancellationToken cancellationToken = default)
        {
            Friendship friendship = await _unitOfWork.Friendships.GetByIdAsync(request.FriendshipId);

            return _mapper.Map<Friendship, FriendshipViewModel>(friendship);
        }
    }
}