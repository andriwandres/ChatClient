using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.ViewModels.Friendships;

namespace Core.Application.Requests.Friendships.Commands;

public class RequestFriendshipCommand : IRequest<FriendshipViewModel>
{
    public int AddresseeId { get; set; }

    public class Handler : IRequestHandler<RequestFriendshipCommand, FriendshipViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateProvider _dateProvider;
        private readonly IUserProvider _userProvider;

        public Handler(IUserProvider userProvider, IUnitOfWork unitOfWork, IDateProvider dateProvider, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
            _userProvider = userProvider;
        }

        public async Task<FriendshipViewModel> Handle(RequestFriendshipCommand request, CancellationToken cancellationToken = default)
        {
            int userId = _userProvider.GetCurrentUserId();

            Friendship friendship = new Friendship
            {
                RequesterId = userId,
                AddresseeId = request.AddresseeId
            };

            FriendshipChange change = new FriendshipChange
            {
                Created = _dateProvider.UtcNow(),
                Status = FriendshipStatus.Pending,
            };

            friendship.StatusChanges.Add(change);
                
            await _unitOfWork.Friendships.Add(friendship, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            FriendshipViewModel resource = _mapper.Map<Friendship, FriendshipViewModel>(friendship);

            return resource;
        }
    }
}