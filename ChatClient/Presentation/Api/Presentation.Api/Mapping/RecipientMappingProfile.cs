﻿using AutoMapper;
using Core.Domain.Entities;
using System.Linq;
using Core.Domain.ViewModels.Groups;
using Core.Domain.ViewModels.Messages;
using Core.Domain.ViewModels.Recipients;
using Core.Domain.ViewModels.Users;

namespace Presentation.Api.Mapping;

public class RecipientMappingProfile : Profile
{
    public RecipientMappingProfile()
    {
        int userId = default;
        CreateMap<MessageRecipient, RecipientViewModel>()
            .ForMember(destination => destination.AvailabilityStatus, config =>
            {
                config.MapFrom(source =>
                    source.Recipient.UserId == null ? 0 : source.Recipient.User.Availability.Status
                );
            })
            .ForMember(destination => destination.UnreadMessagesCount, config =>
            {
                config.MapFrom(source => source.Recipient.ReceivedMessages.Count(
                    mr => mr.IsRead == false && (
                        mr.Recipient.GroupMembershipId == null ||
                        mr.Recipient.GroupMembership.UserId == userId
                    )
                ));
            })
            .ForMember(destination => destination.IsPinned, config =>
            {
                config.MapFrom(source => source.Recipient.Pins.Any(pin => pin.UserId == userId));
            })
            .ForMember(destination => destination.LatestMessage, config =>
            {
                config.MapFrom(source => new LatestMessageViewModel
                {
                    MessageId = source.MessageId,
                    MessageRecipientId = source.MessageRecipientId,
                    AuthorId = source.Message.AuthorId,
                    AuthorName = source.Message.Author.UserName,
                    HtmlContent = source.Message.HtmlContent,
                    IsRead = source.IsRead,
                    Created = source.Message.Created,
                    IsOwnMessage = source.Message.AuthorId == userId
                });
            })
            .ForMember(destination => destination.TargetGroup, config =>
            {
                config.MapFrom(source => source.Recipient.GroupMembershipId == null
                    ? null
                    : new TargetGroupViewModel
                    {
                        GroupId = source.Recipient.GroupMembership.GroupId,
                        Name = source.Recipient.GroupMembership.Group.Name,
                    });
            })
            .ForMember(destination => destination.TargetUser, config =>
            {
                config.MapFrom(source => source.Recipient.UserId == null
                    ? null
                    : new TargetUserViewModel
                    {
                        UserId = source.Recipient.UserId == userId
                            ? source.Message.AuthorId
                            : source.Recipient.User.UserId,
                            
                        UserName = source.Recipient.UserId == userId
                            ? source.Message.Author.UserName
                            : source.Recipient.User.UserName
                    });
            });
    }
}