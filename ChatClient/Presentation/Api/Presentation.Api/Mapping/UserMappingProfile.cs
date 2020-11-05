using AutoMapper;
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
            CreateMap<User, AuthenticatedUser>()
                .ForMember(destination => destination.Token, config => config.Ignore());

            CreateMap<UserNameExistsDto, UserNameExistsQuery>();
            CreateMap<EmailExistsDto, EmailExistsQuery>();
        }
    }
}
