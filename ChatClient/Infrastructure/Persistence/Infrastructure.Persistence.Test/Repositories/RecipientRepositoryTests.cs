using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories;

public class RecipientRepositoryTests
{
    private readonly IChatContext _context;

    public RecipientRepositoryTests()
    {
        _context = TestContextFactory.Create();
    }

    #region GetById()

    [Fact]
    public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotMatch()
    {
        // Arrange
        const int recipientId = 5431;

        IRecipientRepository repository = new RecipientRepository(_context);

        // Act
        Recipient recipient = await repository.GetByIdAsync(recipientId);

        // Assert
        Assert.Null(recipient);
    }

    [Fact]
    public async Task GetById_ShouldReturnQueryableWithSingleRecipient_WhenIdMatches()
    {
        // Arrange
        const int recipientId = 2;

        Recipient[] recipients = 
        {
            new() { RecipientId = 1 },
            new() { RecipientId = 2 },
        };

        await _context.Recipients.AddRangeAsync(recipients);
        await _context.SaveChangesAsync();

        IRecipientRepository repository = new RecipientRepository(_context);

        // Act
        Recipient recipient = await repository.GetByIdAsync(recipientId);

        // Assert
        Assert.NotNull(recipient);
        Assert.Equal(recipientId, recipient.RecipientId);
    }

    #endregion

    #region Exists()

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenRecipientExists()
    {
        // Arrange
        const int recipientId = 1;

        Recipient[] recipients =
        {
            new() { RecipientId = 1 },
            new() { RecipientId = 2 },
        };

        await _context.Recipients.AddRangeAsync(recipients);
        await _context.SaveChangesAsync();

        IRecipientRepository repository = new RecipientRepository(_context);

        // Act
        bool exists = await repository.Exists(recipientId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenRecipientDoesNotExist()
    {
        // Arrange
        const int recipientId = 341;

        Recipient[] recipients =
        {
            new() { RecipientId = 1 },
            new() { RecipientId = 2 },
        };

        await _context.Recipients.AddRangeAsync(recipients);
        await _context.SaveChangesAsync();

        IRecipientRepository repository = new RecipientRepository(_context);

        // Act
        bool exists = await repository.Exists(recipientId);

        // Assert
        Assert.False(exists);
    }

    #endregion

    #region Add()

    [Fact]
    public async Task Add_ShouldAddRecipientsToTheDbContext()
    {
        // Arrange
        Recipient recipient = new();

        IRecipientRepository repository = new RecipientRepository(_context);

        // Act
        await repository.Add(recipient);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        Assert.NotEqual(0, recipient.RecipientId);
        Recipient addedRecipient = await _context.Recipients.FindAsync(recipient.RecipientId);

        Assert.NotNull(addedRecipient);
    }

    #endregion

    // Test data from the perspective of user #1
    private readonly IEnumerable<MessageRecipient> _testData = new[]
    {
        #region Recipient 1 - Private Chat with User #2 (1 unread message)
        // User #1 sends private message to user #2
        new MessageRecipient
        {
            MessageRecipientId = 1,
            MessageId = 1,
            RecipientId = 2,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 1,
                AuthorId = 1,
                Author = new User
                {
                    UserId = 1,
                    UserName = "user1"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 0, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 2,
                UserId = 2,
                User = new User
                {
                    UserId = 2,
                    UserName = "someOtherUsername",
                    Availability = new Availability
                    {
                        Status = AvailabilityStatus.Online
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // User #2 responds to private message from user #1
        new MessageRecipient
        {
            MessageRecipientId = 2,
            MessageId = 2,
            RecipientId = 1,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 2,
                AuthorId = 2,
                Author = new User
                {
                    UserId = 2,
                    UserName = "user2"
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
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // User #2 sends message to user #1 (has NOT been read)
        new MessageRecipient
        {
            MessageRecipientId = 3,
            MessageId = 3,
            RecipientId = 1,
            IsRead = false,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 3,
                AuthorId = 2,
                Author = new User
                {
                    UserId = 2,
                    UserName = "user2"
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
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        #endregion

        #region Recipient 2 - Private Chat with User #3 (no unread messages)
        // User #3 sends private message to user #1
        new MessageRecipient
        {
            MessageRecipientId = 4,
            MessageId = 4,
            RecipientId = 1,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 4,
                AuthorId = 3,
                Author = new User
                {
                    UserId = 3,
                    UserName = "user3"
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
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // User #1 sends private message to user #3
        new MessageRecipient
        {
            MessageRecipientId = 5,
            MessageId = 5,
            RecipientId = 3,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 5,
                AuthorId = 1,
                Author = new User
                {
                    UserId = 1,
                    UserName = "user1"
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
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // User #1 sends private message to user #3 [unread]
        new MessageRecipient
        {
            MessageRecipientId = 6,
            MessageId = 6,
            RecipientId = 3,
            IsRead = false,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 6,
                AuthorId = 1,
                Author = new User
                {
                    UserId = 1,
                    UserName = "user1"
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
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        #endregion

        #region Recipient 3 - Group Chat with User #2 and User #3 (2 unread messages)
        // User #1 sends group message to group #1 (3x recipients)
        // - recipient: user #1 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 7,
            MessageId = 7,
            RecipientId = 4,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 7,
                AuthorId = 1,
                Author = new User
                {
                    UserId = 1,
                    UserName = "user1"
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
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // - recipient: user #2 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 8,
            MessageId = 7,
            RecipientId = 5,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 7,
                AuthorId = 1,
                Author = new User
                {
                    UserId = 1,
                    UserName = "user1"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 1, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 5,
                GroupMembershipId = 2,
                GroupMembership = new GroupMembership
                {
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        GroupId = 1,
                        Name = "group1"
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        }, 
        // - recipient: user #3 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 9,
            MessageId = 7,
            RecipientId = 6,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 7,
                AuthorId = 1,
                Author = new User
                {
                    UserId = 1,
                    UserName = "user1"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 1, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 6,
                GroupMembershipId = 3,
                GroupMembership = new GroupMembership
                {
                    GroupId = 1,
                    UserId = 3,
                    Group = new Group
                    {
                        GroupId = 1,
                        Name = "group1"
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        }, 

        // User #2 sends group message to group #1 (3x recipients)
        // - recipient: user #2 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 10,
            MessageId = 8,
            RecipientId = 5,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 8,
                AuthorId = 2,
                Author = new User
                {
                    UserId = 2,
                    UserName = "user2"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 1, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 5,
                GroupMembershipId = 2,
                GroupMembership = new GroupMembership
                {
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        GroupId = 1,
                        Name = "group1"
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // - recipient: user #1 (group membership) [unread]
        new MessageRecipient
        {
            MessageRecipientId = 11,
            MessageId = 8,
            RecipientId = 4,
            IsRead = false,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 8,
                AuthorId = 2,
                Author = new User
                {
                    UserId = 2,
                    UserName = "user2"
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
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // - recipient: user #3 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 12,
            MessageId = 8,
            RecipientId = 6,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 8,
                AuthorId = 2,
                Author = new User
                {
                    UserId = 2,
                    UserName = "user2"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 1, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 6,
                GroupMembershipId = 3,
                GroupMembership = new GroupMembership
                {
                    GroupId = 1,
                    UserId = 3,
                    Group = new Group
                    {
                        GroupId = 1,
                        Name = "group1"
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },

        // User #3 sends group message to group #1 (3x recipients)
        // - recipient: user #3 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 13,
            MessageId = 9,
            RecipientId = 6,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 9,
                AuthorId = 3,
                Author = new User
                {
                    UserId = 3,
                    UserName = "user3"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 1, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 6,
                GroupMembershipId = 3,
                GroupMembership = new GroupMembership
                {
                    GroupId = 1,
                    UserId = 3,
                    Group = new Group
                    {
                        GroupId = 1,
                        Name = "group1"
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // - recipient: user #1 (group membership) [unread]
        new MessageRecipient
        {
            MessageRecipientId = 14,
            MessageId = 9,
            RecipientId = 4,
            IsRead = false,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 9,
                AuthorId = 3,
                Author = new User
                {
                    UserId = 3,
                    UserName = "user3"
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
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        // - recipient: user #2 (group membership)
        new MessageRecipient
        {
            MessageRecipientId = 15,
            MessageId = 9,
            RecipientId = 5,
            IsRead = true,
            IsForwarded = false,
            Message = new Message
            {
                MessageId = 9,
                AuthorId = 3,
                Author = new User
                {
                    UserId = 3,
                    UserName = "user3"
                },
                HtmlContent = "messageContent",
                Created = new DateTime(2020, 2, 2, 15, 1, 0),
            },
            Recipient = new Recipient
            {
                RecipientId = 5,
                GroupMembershipId = 2,
                GroupMembership = new GroupMembership
                {
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        GroupId = 1,
                        Name = "group1"
                    }
                },
                Pins = new List<PinnedRecipient>(),
                ReceivedMessages = new List<MessageRecipient>(),
            },
        },
        #endregion
    };
}