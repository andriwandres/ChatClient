using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Resources.Friendships;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Requests.Friendships.Commands
{
    public class RequestFriendshipCommand : IRequest<FriendshipResource>
    {
        public int AddresseeId { get; set; }

        public class Handler : IRequestHandler<RequestFriendshipCommand, FriendshipResource>
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

            public async Task<FriendshipResource> Handle(RequestFriendshipCommand request, CancellationToken cancellationToken = default)
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

                FriendshipResource resource = _mapper.Map<Friendship, FriendshipResource>(friendship);

                return resource;
            }
        }
    }
}
