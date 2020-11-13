using AutoMapper;
using Core.Application.Requests.Friendships.Commands;
using Core.Domain.Dtos.Friendships;
using Core.Domain.Entities;
using Core.Domain.Resources.Friendships;

namespace Presentation.Api.Mapping
{
    public class FriendshipMappingProfile : Profile
    {
        public FriendshipMappingProfile()
        {
            CreateMap<Friendship, FriendshipResource>();
            CreateMap<RequestFriendshipDto, RequestFriendshipCommand>();
        }
    }
}
