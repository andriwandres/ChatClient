using AutoMapper;
using Core.Application.Requests.GroupMemberships.Commands;
using Core.Application.Requests.GroupMemberships.Queries;
using Core.Domain.Dtos.GroupMemberships;
using Core.Domain.Entities;
using Core.Domain.Resources.GroupMemberships;

namespace Presentation.Api.Mapping;

public class GroupMembershipMappingProfile : Profile
{
    public GroupMembershipMappingProfile()
    {
        CreateMap<GroupMembership, GroupMembershipResource>()
            .ForMember(resource => resource.UserName, config =>
                config.MapFrom(source => source.User.UserName));

        CreateMap<CreateMembershipBody, CreateMembershipCommand>();
        CreateMap<CreateMembershipBody, MembershipCombinationExistsQuery>();
    }
}