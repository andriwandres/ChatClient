using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Database;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Requests.Friendships.Commands
{
    public class RequestFriendshipCommand : IRequest<FriendshipResource>
    {
        public int AddresseeId { get; set; }

        public class RequestFriendshipCommandHandler : IRequestHandler<RequestFriendshipCommand, FriendshipResource>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDateProvider _dateProvider;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public RequestFriendshipCommandHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IDateProvider dateProvider, IMapper mapper)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _dateProvider = dateProvider;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<FriendshipResource> Handle(RequestFriendshipCommand request, CancellationToken cancellationToken = default)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Friendship friendship = new Friendship
                {
                    RequesterId = userId,
                    AddresseeId = request.AddresseeId
                };

                FriendshipChange change = new FriendshipChange
                {
                    StatusId = 1,
                    Created = _dateProvider.UtcNow()
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
