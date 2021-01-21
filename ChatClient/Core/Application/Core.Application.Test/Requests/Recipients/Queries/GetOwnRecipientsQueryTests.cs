using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Recipients;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries
{
    public class GetOwnRecipientsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public GetOwnRecipientsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);
        }

        // TODO: Split up in various different test methods for clarity in test reports
        [Fact]
        public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsRecipients()
        {
            // Arrange
            IQueryable<MessageRecipient> queryableMock = _testData
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.GetLatestGroupedByRecipients(It.IsAny<int>()))
                .Returns(queryableMock);

            GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);
            
            // Act
            IEnumerable<RecipientResource> recipients = await handler.Handle(new GetOwnRecipientsQuery());

            // Assert
            Assert.Equal(3, recipients.Count());

            // * Mapped recipientId correctly
            Assert.Equal(2, recipients.ElementAt(0).RecipientId);
            Assert.Equal(3, recipients.ElementAt(1).RecipientId);
            Assert.Equal(4, recipients.ElementAt(2).RecipientId);

            // * Mapped targets correctly
            Assert.Null(recipients.ElementAt(0).TargetGroup);
            Assert.NotNull(recipients.ElementAt(0).TargetUser);
            Assert.Equal(2, recipients.ElementAt(0).TargetUser.UserId);

            Assert.Null(recipients.ElementAt(1).TargetGroup);
            Assert.NotNull(recipients.ElementAt(1).TargetUser);
            Assert.Equal(3, recipients.ElementAt(1).TargetUser.UserId);

            Assert.Null(recipients.ElementAt(2).TargetUser);
            Assert.NotNull(recipients.ElementAt(2).TargetGroup);
            Assert.Equal(1, recipients.ElementAt(2).TargetGroup.GroupId);

            // * Mapped latest message correctly
            Assert.NotNull(recipients.ElementAt(0).LatestMessage);
            Assert.Equal(1, recipients.ElementAt(0).LatestMessage.MessageId);

            Assert.NotNull(recipients.ElementAt(1).LatestMessage);
            Assert.Equal(2, recipients.ElementAt(1).LatestMessage.MessageId);

            Assert.NotNull(recipients.ElementAt(2).LatestMessage);
            Assert.Equal(3, recipients.ElementAt(2).LatestMessage.MessageId);

            // * Mapped unread messages correctly
            Assert.Equal(1, recipients.ElementAt(0).UnreadMessagesCount);
            Assert.Equal(0, recipients.ElementAt(1).UnreadMessagesCount);
            Assert.Equal(2, recipients.ElementAt(2).UnreadMessagesCount);

        }

        // TODO: Add pin data & availability variants
        // Test data from the perspective of user #1
        private readonly IEnumerable<MessageRecipient> _testData = new[]
        {
            // Recipient 1 - Private Chat with User #2 (1 unread message)
            new MessageRecipient
            {
                MessageRecipientId = 1,
                MessageId = 1,
                RecipientId = 1,
                IsRead = false,
                IsForwarded = false,
                Message = new Message
                {
                    MessageId = 1,
                    AuthorId = 2,
                    Author = new User
                    {
                        UserId = 2,
                        UserName = "user2",
                        Recipient = new Recipient
                        {
                            RecipientId = 2
                        }
                    },
                    HtmlContent = "messageContent",
                    Created = new DateTime(2020, 2, 2, 15, 0, 0),
                },
                Recipient = new Recipient
                {
                    RecipientId = 1,
                    UserId = 1,
                    User = new User
                    {
                        UserId = 1,
                        UserName = "user1",
                        Availability = new Availability
                        {
                            StatusId = AvailabilityStatusId.Online
                        }
                    },
                    Pins = new List<PinnedRecipient>(),
                    ReceivedMessages = new List<MessageRecipient>
                    {
                        new MessageRecipient
                        {
                            IsRead = true,
                            Message = new Message { AuthorId = 2 }
                        },
                        new MessageRecipient
                        {
                            IsRead = false,
                            Message = new Message { AuthorId = 2 }
                        },
                        new MessageRecipient
                        {
                            IsRead = false,
                            Message = new Message { AuthorId = 8742 }
                        }
                    },
                },
            },

            // Recipient 2 - Private Chat with User #3 (no unread messages)
            new MessageRecipient
            {
                MessageRecipientId = 2,
                MessageId = 2,
                RecipientId = 3,
                IsRead = false,
                IsForwarded = false,
                Message = new Message
                {
                    MessageId = 2,
                    AuthorId = 1,
                    Author = new User
                    {
                        UserId = 1,
                        UserName = "user1",
                    },
                    HtmlContent = "messageContent",
                    Created = new DateTime(2020, 2, 2, 15, 0, 0),
                },
                Recipient = new Recipient
                {
                    RecipientId = 3,
                    UserId = 3,
                    User = new User
                    {
                        UserId = 3,
                        UserName = "user3",
                        Availability = new Availability
                        {
                            StatusId = AvailabilityStatusId.Online
                        }
                    },
                    Pins = new List<PinnedRecipient>(),
                    ReceivedMessages = new List<MessageRecipient>(),
                },
            },

            // Recipient 3 - Group Chat with User #2 and User #3 (2 unread messages)
            new MessageRecipient
            {
                MessageRecipientId = 3,
                MessageId = 3,
                RecipientId = 4,
                IsRead = false,
                IsForwarded = false,
                Message = new Message
                {
                    MessageId = 3,
                    AuthorId = 3,
                    Author = new User
                    {
                        UserId = 3,
                        UserName = "user3",
                    },
                    HtmlContent = "messageContent",
                    Created = new DateTime(2020, 2, 2, 15, 1, 0),
                },
                Recipient = new Recipient
                {
                    RecipientId = 4,
                    GroupMembershipId = 1,
                    GroupMembership = new GroupMembership
                    {
                        GroupId = 1,
                        UserId = 1,
                        Group = new Group
                        {
                            GroupId = 1,
                            Name = "group1"
                        }
                    },
                    Pins = new List<PinnedRecipient>(),
                    ReceivedMessages = new List<MessageRecipient>
                    {
                        new MessageRecipient
                        {
                            IsRead = true,
                            Message = new Message { AuthorId = 1 }
                        },
                        new MessageRecipient
                        {
                            IsRead = true,
                            Message = new Message { AuthorId = 2 }
                        },
                        new MessageRecipient
                        {
                            IsRead = false,
                            Message = new Message { AuthorId = 2 }
                        },
                        new MessageRecipient
                        {
                            IsRead = false,
                            Message = new Message { AuthorId = 3 }
                        },
                    },
                },
            },
        };
    }
}
