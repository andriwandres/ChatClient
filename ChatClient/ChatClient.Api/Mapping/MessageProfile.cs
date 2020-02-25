using AutoMapper;
using ChatClient.Core.Models.Domain;
using ChatClient.Core.Models.ViewModels.Message;
using System.Linq;

namespace ChatClient.Api.Mapping
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageRecipient, LatestMessageViewModel>()
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
                .ForMember(vm => vm.AuthorId, config =>
                {
                    config.MapFrom(mr => mr.Message.AuthorId);
                })
                .ForMember(vm => vm.AuthorName, config =>
                {
                    config.MapFrom((source, destination, member, context) => source.Message.AuthorId == (int) context.Items["UserId"]
                        ? "You"
                        : source.Message.Author.DisplayName
                    );
                })
                .ForMember(vm => vm.CreatedAt, config =>
                {
                    config.MapFrom(mr => mr.Message.CreatedAt);
                })
                .ForMember(vm => vm.UserRecipient, config =>
                {
                    config.PreCondition(mr => mr.RecipientUser != null);
                    config.MapFrom((source, destination, member, context) => new LatestMessageViewModel.RecipientUser
                    {
                        UserId = source.RecipientUser.UserId == (int) context.Items["UserId"]
                            ? source.Message.AuthorId
                            : source.RecipientUser.UserId,

                        DisplayName = source.RecipientUser.UserId == (int) context.Items["UserId"]
                            ? source.Message.Author.DisplayName
                            : source.RecipientUser.DisplayName,
                    });
                })
                .ForMember(vm => vm.GroupRecipient, config =>
                {
                    config.PreCondition(mr => mr.RecipientGroup != null);
                    config.MapFrom(mr => new LatestMessageViewModel.RecipientGroup
                    {
                        GroupId = mr.RecipientGroup.GroupId,
                        Name = mr.RecipientGroup.Group.Name,
                    });
                });

            CreateMap<MessageRecipient, ChatMessageViewModel>()
                .ForMember(vm => vm.AuthorName, config =>
                {
                    config.MapFrom(mr => mr.Message.Author.DisplayName);
                })
                .ForMember(vm => vm.TextContent, config =>
                {
                    config.MapFrom(mr => mr.Message.TextContent);
                })
                .ForMember(vm => vm.CreatedAt, config =>
                {
                    config.MapFrom(mr => mr.Message.CreatedAt);
                })
                .ForMember(vm => vm.IsOwnMessage, config =>
                {
                    config.MapFrom((source, destination, member, context) => 
                        source.Message.AuthorId == (int)context.Items["UserId"]);
                });
        }
    }
}
