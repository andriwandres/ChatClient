using Core.Application.Database;
using Core.Application.Hubs;
using Core.Application.Requests.Messages.Commands;
using Core.Application.Services;
using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using Core.Domain.Resources.Messages;
using Microsoft.AspNetCore.SignalR;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Commands
{
    public class SendMessageCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDateProvider> _dateProviderMock;
        private readonly Mock<IUserProvider> _userProviderMock;
        private readonly Mock<IHubContext<ChatHubBase, IHubClient>> _hubContextMock;

        public SendMessageCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _dateProviderMock = new Mock<IDateProvider>();
            _userProviderMock = new Mock<IUserProvider>();
            _hubContextMock = new Mock<IHubContext<ChatHubBase, IHubClient>>();

            _dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1));

            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            _hubContextMock
                .Setup(m => m.Clients
                    .User(It.IsAny<string>())
                    .ReceiveMessage(It.IsAny<ReceiveMessagePayload>())
                )
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task SendMessageCommandHandler_ShouldAddMessage_IncludingOneRecipient_WhenMessageIsSentToPrivateChatUser()
        {
            // Arrange
            SendMessageCommand request = new SendMessageCommand
            {
                RecipientId = 2,
                ParentId = 1,
                HtmlContent = "<p>hello world</p>"
            };

            IQueryable<Recipient> databaseRecipient = new[]
            {
                new Recipient
                {
                    RecipientId = request.RecipientId,
                    UserId = 2,
                }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            IQueryable<User> databaseUser = new[]
            {
                new User
                {
                    UserId = 1,
                    Recipient = new Recipient {RecipientId = 2}
                }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Users.GetById(1))
                .Returns(databaseUser);

            _unitOfWorkMock
                .Setup(m => m.Recipients.GetById(2))
                .Returns(databaseRecipient);

            Message passedMessage = null;
            MessageRecipient passedMessageRecipient = null;

            _unitOfWorkMock
                .Setup(m => m.Messages.Add(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
                .Callback<Message, CancellationToken>((m, _) =>
                {
                    passedMessage = m;
                    m.MessageId = 1;
                })
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.Add(It.IsAny<MessageRecipient>(), It.IsAny<CancellationToken>()))
                .Callback<MessageRecipient, CancellationToken>((mr, _) =>
                {
                    passedMessageRecipient = mr;
                    passedMessageRecipient.RecipientId = 1;
                })
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            SendMessageCommand.Handler handler = new SendMessageCommand.Handler(_unitOfWorkMock.Object, _dateProviderMock.Object, _userProviderMock.Object, _hubContextMock.Object);

            // Act
            ChatMessageResource result = await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.Messages.Add(It.IsAny<Message>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(passedMessage);
            Assert.Equal(1, passedMessage.AuthorId);
            Assert.Equal(request.ParentId, passedMessage.ParentId);

            _unitOfWorkMock.Verify(m => m.MessageRecipients.Add(It.IsAny<MessageRecipient>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(passedMessageRecipient);
            Assert.Equal(1, passedMessageRecipient.RecipientId);

            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            _hubContextMock.Verify(m => m.Clients.User("1").ReceiveMessage(It.IsAny<ReceiveMessagePayload>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(1, result.MessageId);
        }

        [Fact]
        public async Task SendMessageCommandHandler_ShouldAddMessage_IncludingMultipleRecipients_WhenMessageIsSentToGroupChat()
        {
            // Arrange
            SendMessageCommand request = new SendMessageCommand
            {
                RecipientId = 1,
                ParentId = 1,
                HtmlContent = "<p>hello world</p>"
            };

            IQueryable<Recipient> databaseRecipient = new[]
            {
                new Recipient
                {
                    RecipientId = request.RecipientId,
                    GroupMembership = new GroupMembership
                    {
                        Group = new Group
                        {
                            Memberships = new HashSet<GroupMembership>
                            {
                                new GroupMembership
                                {
                                    UserId = 1,
                                    Recipient = new Recipient
                                    {
                                        RecipientId = 1,
                                        GroupMembership = new GroupMembership { UserId = 1 }
                                    },
                                },
                                new GroupMembership
                                {
                                    UserId = 2,
                                    Recipient = new Recipient
                                    {
                                        RecipientId = 2,
                                        GroupMembership = new GroupMembership { UserId = 2 }
                                    },
                                }
                            }
                        }
                    },
                }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            IQueryable<User> databaseUser = new[]
            {
                new User
                {
                    UserId = 1,
                    Recipient = new Recipient {RecipientId = 2}
                }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Recipients.GetById(1))
                .Returns(databaseRecipient);

            _unitOfWorkMock
                .Setup(m => m.Users.GetById(1))
                .Returns(databaseUser);

            Message passedMessage = null;
            IEnumerable<MessageRecipient> passedMessageRecipients = null;

            _unitOfWorkMock
                .Setup(m => m.Messages.Add(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
                .Callback<Message, CancellationToken>((m, _) =>
                {
                    passedMessage = m;
                    m.MessageId = 1;
                })
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.AddRange(It.IsAny<IEnumerable<MessageRecipient>>(), It.IsAny<CancellationToken>()))
                .Callback<IEnumerable<MessageRecipient>, CancellationToken>((mrs, _) => passedMessageRecipients = mrs)
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            SendMessageCommand.Handler handler = new SendMessageCommand.Handler(_unitOfWorkMock.Object, _dateProviderMock.Object, _userProviderMock.Object, _hubContextMock.Object);

            // Act
            ChatMessageResource result = await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.Messages.Add(It.IsAny<Message>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(passedMessage);
            Assert.Equal(1, passedMessage.AuthorId);
            Assert.Equal(request.ParentId, passedMessage.ParentId);

            _unitOfWorkMock.Verify(m => m.MessageRecipients.AddRange(It.IsAny<IEnumerable<MessageRecipient>>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotEmpty(passedMessageRecipients);
            Assert.Equal(2, passedMessageRecipients.Count());

            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            _hubContextMock.Verify(m => m.Clients.User("1").ReceiveMessage(It.IsAny<ReceiveMessagePayload>()), Times.Exactly(1));

            Assert.NotNull(result);
            Assert.Equal(1, result.MessageId);
        }
    }
}
