using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Contact;

namespace ChatClient.Api.Mapping
{
    public class UserRelationshipProfile : Profile
    {
        public UserRelationshipProfile()
        {
            CreateMap<UserRelationship, ContactViewModel>()
                .ForMember(vm => vm.UserId, config =>
                {
                    config.MapFrom((relationship, viewmodel, member, context) => {
                        return relationship.InitiatorId == (int)context.Items["UserId"]
                            ? relationship.TargetId
                            : relationship.InitiatorId;
                    });
                })
                .ForMember(vm => vm.DisplayName, config =>
                {
                    config.MapFrom((relationship, viewmodel, member, context) =>
                    {
                        return relationship.InitiatorId == (int)context.Items["UserId"]
                            ? relationship.Target.DisplayName
                            : relationship.Initiator.DisplayName;
                    });
                })
                .ForMember(vm => vm.UserCode, config =>
                {
                    config.MapFrom((relationship, viewmodel, member, context) =>
                    {
                        return relationship.InitiatorId == (int)context.Items["UserId"]
                            ? relationship.Target.UserCode
                            : relationship.Initiator.UserCode;
                    });
                });
        }
    }
}
