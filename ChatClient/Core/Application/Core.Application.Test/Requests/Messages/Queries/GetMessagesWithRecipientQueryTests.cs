using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Services;
using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using Core.Domain.Resources.Messages;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class GetMessagesWithRecipientQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;
        private readonly IMapper _mapperMock;

        public GetMessagesWithRecipientQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<GetMessagesWithRecipientQuery, MessageBoundaries>();
            });

            _mapperMock = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetMessagesWithRecipientQueryHandler_ShouldReturnMessages()
        {
            // Arrange
            GetMessagesWithRecipientQuery request = new GetMessagesWithRecipientQuery { RecipientId = 1, Limit = 50, Before = null, After = null };

            IEnumerable<MessageRecipient> expectedMessageRecipients = new[]
            {
                new MessageRecipient
                {
                    MessageRecipientId = 1,
                    MessageId = 1,
                    Message = new Message
                    {
                        MessageId = 1,
                        AuthorId = 1,
                        HtmlContent = "messageContent1",
                        Created = new DateTime(2020, 2, 8, 15, 0, 0),
                        Author = new User
                        {
                            UserId = 1,
                            UserName = "someUsername"
                        },
                        MessageRecipients = new List<MessageRecipient>
                        {
                            new MessageRecipient { IsRead = true },
                        }
                    }
                },
                new MessageRecipient
                {
                    MessageRecipientId = 2,
                    MessageId = 2,
                    Message = new Message
                    {
                        MessageId = 2,
                        AuthorId = 2,
                        HtmlContent = "messageContent2",
                        Created = new DateTime(2020, 2, 8, 15, 3, 0),
                        Author = new User
                        {
                            UserId = 2,
                            UserName = "someOtherUsername"
                        },
                        MessageRecipients = new List<MessageRecipient>
                        {
                            new MessageRecipient { IsRead = true },
                        }
                    }
                },
            };

            IQueryable<MessageRecipient> queryableMock = expectedMessageRecipients
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.GetMessagesWithRecipient(1, request.RecipientId, It.IsAny<MessageBoundaries>()))
                .Returns(queryableMock);

            GetMessagesWithRecipientQuery.Handler handler = new GetMessagesWithRecipientQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object, _mapperMock);

            // Act
            IEnumerable<ChatMessageResource> result = await handler.Handle(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
