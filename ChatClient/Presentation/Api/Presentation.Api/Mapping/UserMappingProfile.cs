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

            CreateMap<User, UserProfileResource>()
                .ForMember(destination => destination.AvailabilityStatus, config =>
                {
                    config.MapFrom(source => source.Availability.Status);
                });

            CreateMap<CreateAccountBody, CreateAccountCommand>();
            CreateMap<CreateAccountBody, UserNameOrEmailExistsQuery>();

            CreateMap<UserExistsQueryParams, UserNameOrEmailExistsQuery>();
        }
    }
}
