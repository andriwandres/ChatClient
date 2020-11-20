using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Resources.GroupMemberships;

namespace Presentation.Api.Mapping
{
    public class GroupMembershipMappingProfile : Profile
    {
        public GroupMembershipMappingProfile()
        {
            CreateMap<GroupMembership, GroupMembershipResource>()
                .ForMember(resource => resource.UserName, config =>
                    config.MapFrom(source => source.User.UserName));
        }
    }
}
