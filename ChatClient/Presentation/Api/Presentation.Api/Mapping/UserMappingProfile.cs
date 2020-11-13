using AutoMapper;
using Core.Application.Requests.Users.Commands;
using Core.Application.Requests.Users.Queries;
using Core.Domain.Dtos.Users;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;

namespace Presentation.Api.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, AuthenticatedUserResource>()
                .ForMember(destination => destination.Token, 
                    config => config.Ignore());

            CreateMap<User, UserProfileResource>();

            CreateMap<UserNameExistsDto, UserNameExistsQuery>();
            CreateMap<EmailExistsDto, EmailExistsQuery>();

            CreateMap<RegisterUserDto, RegisterUserCommand>();
            CreateMap<RegisterUserDto, UserNameOrEmailExistsQuery>();
        }
    }
}
