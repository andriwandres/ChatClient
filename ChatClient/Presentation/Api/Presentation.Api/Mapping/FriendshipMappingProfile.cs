using AutoMapper;
using Core.Application.Requests.Friendships.Commands;
using Core.Domain.Dtos.Friendships;
using Core.Domain.Entities;
using Core.Domain.ViewModels.Friendships;

namespace Presentation.Api.Mapping;

public class FriendshipMappingProfile : Profile
{
    public FriendshipMappingProfile()
    {
        CreateMap<Friendship, FriendshipViewModel>();
        CreateMap<RequestFriendshipBody, RequestFriendshipCommand>();
    }
}