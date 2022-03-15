﻿using AutoMapper;
using Core.Application.Requests.Groups.Commands;
using Core.Domain.Dtos.Groups;
using Core.Domain.Entities;
using Core.Domain.Resources.Groups;

namespace Presentation.Api.Mapping;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<Group, GroupResource>();
        CreateMap<CreateGroupBody, CreateGroupCommand>();
        CreateMap<UpdateGroupBody, UpdateGroupCommand>(MemberList.Source);
    }
}