using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Queries;

public class GetOwnFriendshipsQuery : IRequest<IEnumerable<FriendshipViewModel>>
{
    public class Handler : IRequestHandler<GetOwnFriendshipsQuery, IEnumerable<FriendshipViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper, IUserProvider userProvider)
        {
            _mapper = mapper;
            _userProvider = userProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FriendshipViewModel>> Handle(GetOwnFriendshipsQuery request, CancellationToken cancellationToken = default)
        {
            // Get the current users id
            int userId = _userProvider.GetCurrentUserId();

            List<Friendship> friendships = await _unitOfWork.Friendships.GetByUser(userId);
                
            return _mapper.Map<List<Friendship>, List<FriendshipViewModel>>(friendships);
        }
    }
}