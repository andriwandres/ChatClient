using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatClient.Api.Mapping
{
    public class MessageProfile : Profile
    {
        public int GetUnreadMessagesCount(MessageRecipient recipient)
        {
            int userId = 0;

            if (recipient.RecipientUser != null)
            {
                return recipient.RecipientUser.ReceivedPrivateMessages.Count(mr => 
                    mr.Message.AuthorId == recipient.Message.AuthorId &&
                    mr.RecipientUserId == userId &&
                    mr.IsRead == false
                );
            }
            
            return recipient.RecipientGroup.ReceivedGroupMessages.Count(mr =>mr.IsRead == false);
        }

        public MessageProfile()
        {
            CreateMap<MessageRecipient, LatestMessage>()
                .ForMember(vm => vm.UnreadMessagesCount, config =>
                {
                    config.MapFrom(recipient => recipient.RecipientUser == null
                        ? recipient.RecipientGroup.ReceivedGroupMessages.Count(mr => mr.IsRead == false)
                        : recipient.RecipientUser.ReceivedPrivateMessages.Count(mr =>
                            mr.IsRead == false &&
                            mr.Message.AuthorId == recipient.Message.AuthorId
                          )
                    );
                })
                .ForMember(vm => vm.TextContent, config =>
                {
                    config.MapFrom(mr => mr.Message.TextContent);
                })
                .ForMember(vm => vm.AuthorName, config =>
                {
                    config.MapFrom(mr => mr.Message.Author.DisplayName);
                })
                .ForMember(vm => vm.CreatedAt, config =>
                {
                    config.MapFrom(mr => mr.Message.CreatedAt);
                })
                .ForMember(vm => vm.UserRecipient, config =>
                {
                    config.Condition(mr => mr.RecipientUser != null);
                    config.MapFrom(mr => new LatestMessage.RecipientUser
                    {
                        UserId = mr.RecipientUser.UserId,
                        DisplayName = mr.RecipientUser.DisplayName,
                        IsOnline = true, // TODO refactor
                    });
                })
                .ForMember(vm => vm.GroupRecipient, config =>
                {
                    config.Condition(mr => mr.RecipientGroup != null);
                    config.MapFrom(mr => new LatestMessage.RecipientGroup
                    {
                        GroupId = mr.RecipientGroup.GroupId,
                        Name = mr.RecipientGroup.Group.Name,
                    });
                });
        }
    }
}
