using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.Dtos.Auth;
using ChatClient.Core.Models.ViewModels.Auth;

namespace ChatClient.Api.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterDto>();

            CreateMap<User, AuthenticatedUser>()
                .ForMember(vm => vm.Token, config =>
                {
                    config.MapFrom((src, dest, member, context) => context.Items["Token"]);
                });
        }
    }
}
