using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Resources.Recipients;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries;

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

        _unitOfWorkMock
            .Setup(m => m.MessageRecipients.GetLatestGroupedByRecipients(1))
            .ReturnsAsync(_testData);
    }

    [Fact]
    public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsRecipientIds()
    {
        // Arrange
        GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        List<RecipientResource> recipients = (await handler.Handle(new GetOwnRecipientsQuery())).ToList();

        // Assert
        Assert.Equal(3, recipients.Count);

        Assert.Equal(2, recipients.ElementAt(0).RecipientId);
        Assert.Equal(3, recipients.ElementAt(1).RecipientId);
        Assert.Equal(4, recipients.ElementAt(2).RecipientId);
    }

    [Fact]
    public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsTargets()
    {
        // Arrange
        GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        List<RecipientResource> recipients = (await handler.Handle(new GetOwnRecipientsQuery())).ToList();

        // Assert
        Assert.Equal(3, recipients.Count);

        Assert.Null(recipients.ElementAt(0).TargetGroup);
        Assert.NotNull(recipients.ElementAt(0).TargetUser);
        Assert.Equal(2, recipients.ElementAt(0).TargetUser.UserId);
        Assert.Equal("user2", recipients.ElementAt(0).TargetUser.UserName);

        Assert.Null(recipients.ElementAt(1).TargetGroup);
        Assert.NotNull(recipients.ElementAt(1).TargetUser);
        Assert.Equal(3, recipients.ElementAt(1).TargetUser.UserId);
        Assert.Equal("user3", recipients.ElementAt(1).TargetUser.UserName);


        Assert.Null(recipients.ElementAt(2).TargetUser);
        Assert.NotNull(recipients.ElementAt(2).TargetGroup);
        Assert.Equal(1, recipients.ElementAt(2).TargetGroup.GroupId);
        Assert.Equal("group1", recipients.ElementAt(2).TargetGroup.Name);
    }

    [Fact]
    public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsLatestMessages()
    {
        // Arrange
        GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        List<RecipientResource> recipients = (await handler.Handle(new GetOwnRecipientsQuery())).ToList();

        // Assert
        Assert.Equal(3, recipients.Count);

        Assert.NotNull(recipients.ElementAt(0).LatestMessage);
        Assert.Equal(1, recipients.ElementAt(0).LatestMessage.MessageId);
        Assert.False(recipients.ElementAt(0).LatestMessage.IsOwnMessage);

        Assert.NotNull(recipients.ElementAt(1).LatestMessage);
        Assert.Equal(2, recipients.ElementAt(1).LatestMessage.MessageId);
        Assert.True(recipients.ElementAt(1).LatestMessage.IsOwnMessage);

        Assert.NotNull(recipients.ElementAt(2).LatestMessage);
        Assert.Equal(3, recipients.ElementAt(2).LatestMessage.MessageId);
        Assert.False(recipients.ElementAt(2).LatestMessage.IsOwnMessage);

    }

    [Fact]
    public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsAvailabilityStatuses()
    {
        // Arrange
        GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        List<RecipientResource> recipients = (await handler.Handle(new GetOwnRecipientsQuery())).ToList();

        // Assert
        Assert.Equal(3, recipients.Count);

        Assert.Equal(AvailabilityStatus.Offline, recipients.ElementAt(0).AvailabilityStatus);
        Assert.Equal(AvailabilityStatus.Online, recipients.ElementAt(1).AvailabilityStatus);
        Assert.Equal((AvailabilityStatus) 0, recipients.ElementAt(2).AvailabilityStatus);
    }

    [Fact]
    public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsPins()
    {
        // Arrange
        GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        List<RecipientResource> recipients = (await handler.Handle(new GetOwnRecipientsQuery())).ToList();

        // Assert
        Assert.Equal(3, recipients.Count);

        Assert.True(recipients.ElementAt(0).IsPinned);
        Assert.False(recipients.ElementAt(1).IsPinned);
        Assert.True(recipients.ElementAt(2).IsPinned);
    }

    [Fact]
    public async Task GetOwnRecipientsQueryHandler_CorrectlyMapsUnreadMessages()
    {
        // Arrange
        GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

        // Act
        List<RecipientResource> recipients = (await handler.Handle(new GetOwnRecipientsQuery())).ToList();

        // Assert
        Assert.Equal(3, recipients.Count);

        Assert.Equal(1, recipients.ElementAt(0).UnreadMessagesCount);
        Assert.Equal(0, recipients.ElementAt(1).UnreadMessagesCount);
        Assert.Equal(2, recipients.ElementAt(2).UnreadMessagesCount);
    }

    // Test data from the perspective of user #1
    private readonly List<MessageRecipient> _testData = new()
    {
        // Recipient 1 - Private Chat with User #2 (1 unread message) [pinned]
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
                        RecipientId = 2,
                        Pins = new List<PinnedRecipient>
                        {
                            new PinnedRecipient { UserId = 378 },
                            new PinnedRecipient { UserId = 1 },
                            new PinnedRecipient { UserId = 3 },
                        }
                    },
                    Availability =  new Availability
                    {
                        Status = AvailabilityStatus.Offline
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
                        Status = AvailabilityStatus.Online
                    }
                },
                Pins = new List<PinnedRecipient>
                {
                    new PinnedRecipient { UserId = 378 },
                    new PinnedRecipient { UserId = 3 },
                },
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
                    Recipient = new Recipient
                    {
                        Pins = new List<PinnedRecipient>
                        {
                            new PinnedRecipient { UserId = 431 },
                            new PinnedRecipient { UserId = 51 },
                        }
                    }
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
                        Status = AvailabilityStatus.Online
                    }
                },
                Pins = new List<PinnedRecipient>
                {
                    new PinnedRecipient { UserId = 431 },
                    new PinnedRecipient { UserId = 51 },
                },
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
                        Message = new Message { AuthorId = 1 }
                    }
                },
            },
        },

        // Recipient 3 - Group Chat with User #2 and User #3 (2 unread messages) [pinned]
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
                    Recipient = new Recipient
                    {
                        Pins = new List<PinnedRecipient>
                        {
                            new PinnedRecipient { UserId = 431 },
                            new PinnedRecipient { UserId = 51 },
                        }
                    }
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
                Pins = new List<PinnedRecipient>
                {
                    new PinnedRecipient { UserId = 431 },
                    new PinnedRecipient { UserId = 51 },
                    new PinnedRecipient { UserId = 1 },
                },
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