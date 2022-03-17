using AutoMapper;
using Core.Application.Requests.Messages.Commands;
using Core.Application.Requests.Messages.Queries;
using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using System.Linq;
using Core.Domain.ViewModels.Messages;

namespace Presentation.Api.Mapping;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        // Define parameters
        int userId = default;

        CreateMap<Message, MessageViewModel>()
            .ForMember(destination => destination.AuthorName, config =>
            {
                config.MapFrom(source => source.Author.UserName);
            })
            .ForMember(destination => destination.ReadDate, config =>
            {
                config.MapFrom(source => 
                    source.MessageRecipients.All(mr => mr.IsRead)
                        ? source.MessageRecipients.Max(mr => mr.ReadDate)
                        : null
                );
            })
            .ForMember(destination => destination.IsRead, config =>
            {
                config.MapFrom(source => source.MessageRecipients.All(mr => mr.IsRead));
            })
            .ForMember(destination => destination.IsOwnMessage, config =>
            {
                config.MapFrom(source => source.AuthorId == userId);
            })
            .ForMember(destination => destination.MessageRecipientId, config =>
            {
                config.MapFrom(source => 
                    source.MessageRecipients
                        .First(mr => (mr.Recipient.UserId ?? mr.Recipient.GroupMembership.UserId) == userId)
                        .MessageRecipientId
                );
            });

        CreateMap<GetMessagesWithRecipientQuery, MessageBoundaries>();
        CreateMap<SendMessageBody, SendMessageCommand>();
    }
}