using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Requests.Session.Commands;
using Core.Domain.Dtos.Session;

namespace Presentation.Api.Mapping;

public class SessionMappingProfile : Profile
{
    public SessionMappingProfile()
    {
        CreateMap<LoginBody, LoginCommand>();
    }
}