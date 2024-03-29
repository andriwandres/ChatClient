﻿using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Dtos.Messages;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories;

public class MessageRecipientRepositoryTests
{
    private readonly IChatContext _context;

    public MessageRecipientRepositoryTests()
    {
        _context = TestContextFactory.Create();
    }

    #region Add()

    [Fact]
    public async Task Add_ShouldAddRecipient()
    {
        // Arrange
        MessageRecipient messageRecipient = new MessageRecipient();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        await repository.Add(messageRecipient);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
            
        // Assert
        Assert.NotEqual(0, messageRecipient.MessageRecipientId);
        MessageRecipient addedMessageRecipient = await _context.MessageRecipients.FindAsync(messageRecipient.MessageRecipientId);

        Assert.NotNull(addedMessageRecipient);
    }

    #endregion

    #region AddRange()

    [Fact]
    public async Task AddRange_ShouldAddMultipleRecipients()
    {
        // Arrange
        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient(),
            new MessageRecipient(),
        };

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        await repository.AddRange(messageRecipients);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        Assert.All(messageRecipients, mr => Assert.NotEqual(0, mr.MessageRecipientId));


    }

    #endregion

    #region GetLatestGroupedByRecipients()

    [Fact]
    public async Task GetLatestGroupedByRecipients_ShouldIncludeRecipientByDirectMessage_WhenTheUserIsTheAuthorInPrivateMessage()
    {
        // Arrange
        const int userId = 1;

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                Message = new Message { MessageId = 1, AuthorId = 1, Created = new DateTime (2020, 1, 1), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient { UserId = 2 }
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                Message = new Message { MessageId = 2, AuthorId = 2, Created = new DateTime (2020, 1, 2), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient { UserId = 3 }
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                Message = new Message { MessageId = 3, AuthorId = 1, Created = new DateTime (2020, 1, 3), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient { UserId = 3 }
            },
        };

        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetLatestGroupedByRecipients(userId);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
            
        Assert.Equal(3, result.ElementAt(0).MessageRecipientId);
        Assert.Equal(1, result.ElementAt(1).MessageRecipientId);
    }

    [Fact]
    public async Task GetLatestGroupedByRecipients_ShouldIncludeRecipientByDirectMessage_WhenTheUserIsTheRecipientInPrivateMessage()
    {
        // Arrange
        const int userId = 1;

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                Message = new Message { MessageId = 1, AuthorId = 2, Created = new DateTime (2020, 1, 1), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient { UserId = 1 } // User as recipient
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                Message = new Message { MessageId = 2, AuthorId = 3, Created = new DateTime (2020, 1, 2), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient { UserId = 2 }
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                Message = new Message { MessageId = 3, AuthorId = 3, Created = new DateTime (2020, 1, 3), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient { UserId = 1 } // User as recipient
            },
        };

        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetLatestGroupedByRecipients(userId);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());

        Assert.Equal(3, result.ElementAt(0).MessageRecipientId);
        Assert.Equal(1, result.ElementAt(1).MessageRecipientId);
    }

    [Fact]
    public async Task GetLatestGroupedByRecipients_ShouldIncludeRecipientByGroupMessage_WhenTheUserIsMemberOfTheGroup()
    {
        // Arrange
        const int userId = 1;

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            // User is author in message to group 1
            new MessageRecipient
            {
                MessageRecipientId = 1,
                Message = new Message { MessageId = 1, AuthorId = 1, Created = new DateTime (2020, 1, 1), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient
                {
                    GroupMembershipId = 1,
                    GroupMembership = new GroupMembership
                    {
                        GroupId = 1,
                        GroupMembershipId = 1, UserId = 1,
                    }
                }
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                Message = new Message { MessageId = 2, AuthorId = 1, Created = new DateTime (2020, 1, 1), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient
                {
                    GroupMembershipId = 2,
                    GroupMembership = new GroupMembership
                    {
                        GroupId = 1,
                        GroupMembershipId = 2, UserId = 2,
                    }
                }
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                Message = new Message { MessageId = 3, AuthorId = 2, Created = new DateTime (2020, 1, 2), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient
                {
                    GroupMembershipId = 3,
                    GroupMembership = new GroupMembership
                    {
                        GroupId = 2,
                        GroupMembershipId = 3, UserId = 2,
                    }
                }
            },
            // User is recipient in message to group 2
            new MessageRecipient
            {
                MessageRecipientId = 4,
                Message = new Message { MessageId = 4, AuthorId = 2, Created = new DateTime (2020, 1, 2), HtmlContent = "<p>Hello World</p>" },
                Recipient = new Recipient
                {
                    GroupMembershipId = 4,
                    GroupMembership = new GroupMembership
                    {
                        GroupId = 2,
                        GroupMembershipId = 4, UserId = 1,
                    }
                }
            },
        };

        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetLatestGroupedByRecipients(userId);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());

        Assert.Equal(4, result.ElementAt(0).MessageRecipientId);
        Assert.Equal(1, result.ElementAt(1).MessageRecipientId);
    }

    #endregion

    #region GetMessagesWithRecipient()

    [Fact]
    public async Task GetMessagesWithRecipient_ShouldIncludeMessage_WhenUserIsRecipientOfPrivateMessage()
    {
        // Arrange
        const int userId = 1;
        const int recipientId = 3;

        MessageBoundaries boundaries = new();

        IEnumerable<User> users = new[]
        {
            new User
            {
                UserId = 1,
                Email = "user1@test.ch",
                UserName = "user1",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
            new User
            {
                UserId = 2,
                Email = "user2@test.ch",
                UserName = "user2",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
        };

        IEnumerable<GroupMembership> groupMemberships = new[]
        {
            new GroupMembership
            {
                GroupMembershipId = 1,
                UserId = 1
            },
            new GroupMembership
            {
                GroupMembershipId = 2,
                UserId = 2
            }
        };

        IEnumerable<Recipient> recipients = new[]
        {
            new Recipient
            {
                RecipientId = 1,
                UserId = 1,
            },
            new Recipient
            {
                RecipientId = 2,
                GroupMembershipId = 1
            },
            new Recipient
            {
                RecipientId = 3,
                UserId = 2,
            },
            new Recipient
            {
                RecipientId = 4,
                GroupMembershipId = 2,
            },
        };

        IEnumerable<Message> messages = new[]
        {
            new Message
            {
                MessageId = 1,
                AuthorId = 1,
                Created = new DateTime (2020, 1, 1),
                HtmlContent = "<p>Hello Friend</p>",
            },
            new Message
            {
                MessageId = 2,
                AuthorId = 2,
                Created = new DateTime (2020, 1, 1),
                HtmlContent = "<p>Hello Friend</p>",
            },
        };

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                RecipientId = 3,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                RecipientId = 1,
                MessageId = 2
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                RecipientId = 2,
                MessageId = 2
            },
            new MessageRecipient
            {
                MessageRecipientId = 4,
                RecipientId = 4,
                MessageId = 2
            },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.GroupMemberships.AddRangeAsync(groupMemberships);
        await _context.Recipients.AddRangeAsync(recipients);
        await _context.Messages.AddRangeAsync(messages);
        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetMessagesWithRecipient(userId, recipientId, boundaries);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());

        Assert.Equal(1, result.ElementAt(0).MessageRecipientId);
        Assert.Equal(2, result.ElementAt(1).MessageRecipientId);
    }

    [Fact]
    public async Task GetMessagesWithRecipient_ShouldIncludeMessage_WhenUserIsRecipientOfGroupMessage()
    {
        // Arrange
        const int userId = 1;
        const int recipientId = 2;

        MessageBoundaries boundaries = new();

        IEnumerable<User> users = new[]
        {
            new User
            {
                UserId = 1,
                Email = "user1@test.ch",
                UserName = "user1",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
            new User
            {
                UserId = 2,
                Email = "user2@test.ch",
                UserName = "user2",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
        };

        IEnumerable<GroupMembership> groupMemberships = new[]
        {
            new GroupMembership
            {
                GroupMembershipId = 1,
                UserId = 1
            },
            new GroupMembership
            {
                GroupMembershipId = 2,
                UserId = 2
            }
        };

        IEnumerable<Recipient> recipients = new[]
        {
            new Recipient
            {
                RecipientId = 1,
                UserId = 1,
            },
            new Recipient
            {
                RecipientId = 2,
                GroupMembershipId = 1
            },
            new Recipient
            {
                RecipientId = 3,
                UserId = 2,
            },
            new Recipient
            {
                RecipientId = 4,
                GroupMembershipId = 2,
            },
        };

        IEnumerable<Message> messages = new[]
        {
            new Message
            {
                MessageId = 1,
                AuthorId = 1,
                Created = new DateTime (2020, 1, 1),
                HtmlContent = "<p>Hello Group</p>",
            },
            new Message
            {
                MessageId = 2,
                AuthorId = 2,
                Created = new DateTime (2020, 1, 1),
                HtmlContent = "<p>Hello Group</p>",
            },
        };

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                RecipientId = 2,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                RecipientId = 4,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                RecipientId = 2,
                MessageId = 2
            },
            new MessageRecipient
            {
                MessageRecipientId = 4,
                RecipientId = 4,
                MessageId = 2
            },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.GroupMemberships.AddRangeAsync(groupMemberships);
        await _context.Recipients.AddRangeAsync(recipients);
        await _context.Messages.AddRangeAsync(messages);
        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetMessagesWithRecipient(userId, recipientId, boundaries);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());

        Assert.Equal(1, result.ElementAt(0).MessageRecipientId);
        Assert.Equal(3, result.ElementAt(1).MessageRecipientId);
    }

    [Fact]
    public async Task GetMessagesWithRecipient_ShouldIncludeMessages_BeforeADate()
    {
        // Arrange
        const int userId = 1;
        const int recipientId = 2;

        MessageBoundaries boundaries = new MessageBoundaries
        {
            Limit = 50,
            Before = new DateTime(2020, 1, 1, 15, 3, 0),
        };

        IEnumerable<User> users = new[]
        {
            new User
            {
                UserId = 1,
                Email = "user1@test.ch",
                UserName = "user1",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
            new User
            {
                UserId = 2,
                Email = "user2@test.ch",
                UserName = "user2",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
        };

        IEnumerable<GroupMembership> groupMemberships = new[]
        {
            new GroupMembership
            {
                GroupMembershipId = 1,
                UserId = 1
            },
            new GroupMembership
            {
                GroupMembershipId = 2,
                UserId = 2
            }
        };

        IEnumerable<Recipient> recipients = new[]
        {
            new Recipient
            {
                RecipientId = 1,
                UserId = 1,
            },
            new Recipient
            {
                RecipientId = 2,
                GroupMembershipId = 1
            },
            new Recipient
            {
                RecipientId = 3,
                UserId = 2,
            },
            new Recipient
            {
                RecipientId = 4,
                GroupMembershipId = 2,
            },
        };

        IEnumerable<Message> messages = new[]
        {
            new Message
            {
                MessageId = 1,
                AuthorId = 1,
                Created = new DateTime(2020, 1, 1, 15, 0, 0),
                HtmlContent = "<p>Hello Group</p>",
            },
            new Message
            {
                MessageId = 2,
                AuthorId = 2,
                Created = new DateTime(2020, 1, 1, 15, 5, 0),
                HtmlContent = "<p>Hello Group</p>",
            },
        };

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                RecipientId = 2,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                RecipientId = 4,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                RecipientId = 2,
                MessageId = 2
            },
            new MessageRecipient
            {
                MessageRecipientId = 4,
                RecipientId = 4,
                MessageId = 2
            },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.GroupMemberships.AddRangeAsync(groupMemberships);
        await _context.Recipients.AddRangeAsync(recipients);
        await _context.Messages.AddRangeAsync(messages);
        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetMessagesWithRecipient(userId, recipientId, boundaries);

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);

        Assert.Equal(1, result.ElementAt(0).MessageRecipientId);
    }

    [Fact]
    public async Task GetMessagesWithRecipient_ShouldIncludeMessages_AfterADate()
    {
        // Arrange
        const int userId = 1;
        const int recipientId = 2;

        MessageBoundaries boundaries = new MessageBoundaries
        {
            Limit = 50,
            After = new DateTime(2020, 1, 1, 15, 3, 0),
        };

        IEnumerable<User> users = new[]
        {
            new User
            {
                UserId = 1,
                Email = "user1@test.ch",
                UserName = "user1",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
            new User
            {
                UserId = 2,
                Email = "user2@test.ch",
                UserName = "user2",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
        };

        IEnumerable<GroupMembership> groupMemberships = new[]
        {
            new GroupMembership
            {
                GroupMembershipId = 1,
                UserId = 1
            },
            new GroupMembership
            {
                GroupMembershipId = 2,
                UserId = 2
            }
        };

        IEnumerable<Recipient> recipients = new[]
        {
            new Recipient
            {
                RecipientId = 1,
                UserId = 1,
            },
            new Recipient
            {
                RecipientId = 2,
                GroupMembershipId = 1
            },
            new Recipient
            {
                RecipientId = 3,
                UserId = 2,
            },
            new Recipient
            {
                RecipientId = 4,
                GroupMembershipId = 2,
            },
        };

        IEnumerable<Message> messages = new[]
        {
            new Message
            {
                MessageId = 1,
                AuthorId = 1,
                Created = new DateTime(2020, 1, 1, 15, 0, 0),
                HtmlContent = "<p>Hello Group</p>",
            },
            new Message
            {
                MessageId = 2,
                AuthorId = 2,
                Created = new DateTime(2020, 1, 1, 15, 5, 0),
                HtmlContent = "<p>Hello Group</p>",
            },
        };

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                RecipientId = 2,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                RecipientId = 4,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                RecipientId = 2,
                MessageId = 2
            },
            new MessageRecipient
            {
                MessageRecipientId = 4,
                RecipientId = 4,
                MessageId = 2
            },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.GroupMemberships.AddRangeAsync(groupMemberships);
        await _context.Recipients.AddRangeAsync(recipients);
        await _context.Messages.AddRangeAsync(messages);
        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetMessagesWithRecipient(userId, recipientId, boundaries);

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);

        Assert.Equal(3, result.ElementAt(0).MessageRecipientId);
    }

    [Fact]
    public async Task GetMessagesWithRecipient_ShouldLimitTheAmountOfLoadedMessages_FromBottomUpwards()
    {
        // Arrange
        const int userId = 1;
        const int recipientId = 2;

        // Define a limit
        MessageBoundaries boundaries = new MessageBoundaries
        {
            Limit = 1,
        };

        IEnumerable<User> users = new[]
        {
            new User
            {
                UserId = 1,
                Email = "user1@test.ch",
                UserName = "user1",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
            new User
            {
                UserId = 2,
                Email = "user2@test.ch",
                UserName = "user2",
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            },
        };

        IEnumerable<GroupMembership> groupMemberships = new[]
        {
            new GroupMembership
            {
                GroupMembershipId = 1,
                UserId = 1
            },
            new GroupMembership
            {
                GroupMembershipId = 2,
                UserId = 2
            }
        };

        IEnumerable<Recipient> recipients = new[]
        {
            new Recipient
            {
                RecipientId = 1,
                UserId = 1,
            },
            new Recipient
            {
                RecipientId = 2,
                GroupMembershipId = 1
            },
            new Recipient
            {
                RecipientId = 3,
                UserId = 2,
            },
            new Recipient
            {
                RecipientId = 4,
                GroupMembershipId = 2,
            },
        };

        IEnumerable<Message> messages = new[]
        {
            new Message
            {
                MessageId = 1,
                AuthorId = 1,
                Created = new DateTime(2020, 1, 1, 15, 0, 0),
                HtmlContent = "<p>Hello Group</p>",
            },
            new Message
            {
                MessageId = 2,
                AuthorId = 2,
                Created = new DateTime(2020, 1, 1, 15, 5, 0),
                HtmlContent = "<p>Hello Group</p>",
            },
        };

        IEnumerable<MessageRecipient> messageRecipients = new[]
        {
            new MessageRecipient
            {
                MessageRecipientId = 1,
                RecipientId = 2,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 2,
                RecipientId = 4,
                MessageId = 1
            },
            new MessageRecipient
            {
                MessageRecipientId = 3,
                RecipientId = 2,
                MessageId = 2
            },
            new MessageRecipient
            {
                MessageRecipientId = 4,
                RecipientId = 4,
                MessageId = 2
            },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.GroupMemberships.AddRangeAsync(groupMemberships);
        await _context.Recipients.AddRangeAsync(recipients);
        await _context.Messages.AddRangeAsync(messages);
        await _context.MessageRecipients.AddRangeAsync(messageRecipients);
        await _context.SaveChangesAsync();

        IMessageRecipientRepository repository = new MessageRecipientRepository(_context);

        // Act
        IEnumerable<MessageRecipient> result = await repository.GetMessagesWithRecipient(userId, recipientId, boundaries);

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);

        Assert.Equal(3, result.ElementAt(0).MessageRecipientId);
    }

    #endregion
}