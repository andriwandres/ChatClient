using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Resources.Recipients;
using System.Linq;
using Core.Domain.Resources.Groups;
using Core.Domain.Resources.Messages;
using Core.Domain.Resources.Users;

namespace Presentation.Api.Mapping
{
    public class RecipientMappingProfile : Profile
    {
        public RecipientMappingProfile()
        {
            int userId = default;
            CreateMap<MessageRecipient, RecipientResource>()
                .ForMember(destination => destination.UnreadMessagesCount, config =>
                {
                    config.MapFrom(source => source.Recipient.ReceivedMessages.Count(
                        mr => mr.IsRead == false && (
                            mr.Recipient.GroupMembershipId == null ||
                            mr.Recipient.GroupMembership.UserId == userId
                        )
                    ));
                })
                .ForMember(destination => destination.LatestMessage, config =>
                {
                    config.MapFrom(source => new LatestMessageResource
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
                        : new TargetGroupResource
                        {
                            GroupId = source.Recipient.GroupMembership.GroupId,
                            Name = source.Recipient.GroupMembership.Group.Name,
                        });
                })
                .ForMember(destination => destination.TargetUser, config =>
                {
                    config.MapFrom(source => source.Recipient.UserId == null
                        ? null
                        : new TargetUserResource
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
}
