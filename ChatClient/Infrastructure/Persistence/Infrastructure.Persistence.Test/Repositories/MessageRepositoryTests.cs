using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories;

public class MessageRepositoryTests
{
    private readonly IChatContext _context;

    public MessageRepositoryTests()
    {
        _context = TestContextFactory.Create();
    }

    #region GetById()

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenIdDoesNotMatch()
    {
        // Arrange
        const int messageId = 5204;

        IEnumerable<Message> expectedMessages = new[]
        {
            new Message { MessageId = 1, HtmlContent = "Test", },
            new Message { MessageId = 2, HtmlContent = "Test", },
            new Message { MessageId = 3, HtmlContent = "Test", },
        };

        await _context.Messages.AddRangeAsync(expectedMessages);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        Message message = await repository.GetByIdAsync(messageId);

        // Assert
        Assert.Null(message);
    }

    [Fact]
    public async Task GetById_ShouldReturnMessage_WhenIdMatches()
    {
        // Arrange
        const int messageId = 2;

        IEnumerable<Message> expectedMessages = new[]
        {
            new Message { MessageId = 1, HtmlContent = "Test", },
            new Message { MessageId = 2, HtmlContent = "Test", },
            new Message { MessageId = 3, HtmlContent = "Test", },
        };

        await _context.Messages.AddRangeAsync(expectedMessages);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        Message message = await repository.GetByIdAsync(messageId);

        // Assert
        Assert.NotNull(message);
        Assert.Equal(messageId, message.MessageId);
    }

    #endregion

    #region Exists()

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenMessageExists()
    {
        // Arrange
        const int messageId = 1;

        IEnumerable<Message> expectedMessages = new[]
        {
            new Message { MessageId = 1, HtmlContent = "Test", },
            new Message { MessageId = 2, HtmlContent = "Test", },
            new Message { MessageId = 3, HtmlContent = "Test", },
        };

        await _context.Messages.AddRangeAsync(expectedMessages);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        bool exists = await repository.Exists(messageId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenMessageDoesNotExist()
    {
        // Arrange
        const int messageId = 89471;

        IEnumerable<Message> expectedMessages = new[]
        {
            new Message { MessageId = 1, HtmlContent = "Test", },
            new Message { MessageId = 2, HtmlContent = "Test", },
            new Message { MessageId = 3, HtmlContent = "Test", },
        };

        await _context.Messages.AddRangeAsync(expectedMessages);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        bool exists = await repository.Exists(messageId);

        // Assert
        Assert.False(exists);
    }

    #endregion

    #region CanAccess()

    [Fact]
    public async Task CanAccess_ShouldReturnTrue_WhenTheUserIsTheAuthorOfTheMessage()
    {
        // Arrange
        const int userId = 1;
        const int messageId = 1;

        Message expectedMessage = new()
        {
            MessageId = 1, 
            AuthorId = 1,
            HtmlContent = "Test",
            MessageRecipients = new List<MessageRecipient>
            {
                new() { Recipient = new Recipient { UserId = 2 } },
                new() { Recipient = new Recipient { UserId = 3 } }
            }
        };

        await _context.Messages.AddAsync(expectedMessage);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        bool canAccess = await repository.CanAccess(messageId, userId);

        // Assert
        Assert.True(canAccess);
    }

    [Fact]
    public async Task CanAccess_ShouldReturnTrue_WhenTheUserHasReceivedTheMessageInAPrivateChat()
    {
        // Arrange
        const int userId = 1;
        const int messageId = 1;

        Message expectedMessage = new()
        { 
            MessageId = 1, 
            AuthorId = 2,
            HtmlContent = "Test",
            MessageRecipients = new List<MessageRecipient>
            {
                new() { Recipient = new Recipient { UserId = 1 }},
                new() { Recipient = new Recipient { UserId = 3 }}
            }
        };

        await _context.Messages.AddAsync(expectedMessage);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        bool canAccess = await repository.CanAccess(messageId, userId);

        // Assert
        Assert.True(canAccess);
    }

    [Fact]
    public async Task CanAccess_ShouldReturnTrue_WhenTheUserHasReceivedTheMessageInAGroupChat()
    {
        // Arrange
        const int userId = 1;
        const int messageId = 1;

        Message expectedMessage =  new()
        {
            MessageId = 1, 
            AuthorId = 2,
            HtmlContent = "Test",
            MessageRecipients = new List<MessageRecipient>
            {
                new() { Recipient = new Recipient { GroupMembership = new GroupMembership { UserId = 3 } } },
                new() { Recipient = new Recipient { GroupMembership = new GroupMembership { UserId = 1 } } },
                new() { Recipient = new Recipient { GroupMembership = new GroupMembership { UserId = 4 } } }
            }
        };

        await _context.Messages.AddAsync(expectedMessage);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        bool canAccess = await repository.CanAccess(messageId, userId);

        // Assert
        Assert.True(canAccess);
    }

    [Fact]
    public async Task CanAccess_ShouldReturnFalse_WhenTheUserHasNeitherSentNorReceivedTheMessage()
    {
        // Arrange
        const int userId = 1;
        const int messageId = 1;

        Message expectedMessage = new()
        {
            MessageId = 1,
            AuthorId = 2,
            HtmlContent = "Test",
            MessageRecipients = new List<MessageRecipient>
            {
                new() { Recipient = new Recipient { UserId = 3 } },
                new() { Recipient = new Recipient { UserId = 4 } }
            }
        };

        await _context.Messages.AddAsync(expectedMessage);
        await _context.SaveChangesAsync();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        bool canAccess = await repository.CanAccess(messageId, userId);

        // Assert
        Assert.False(canAccess);
    }

    #endregion

    #region Add()

    [Fact]
    public async Task Add_ShouldAddMessage()
    {
        // Arrange
        Message message = new();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        await repository.Add(message);

        // Assert
        Assert.NotEqual(0, message.MessageId);
        Message addedMessage = await _context.Messages.FindAsync(message.MessageId);

        Assert.NotNull(addedMessage);
    }

    #endregion

    #region Update()

    [Fact]
    public async Task Update_ShouldUpdateMessage()
    {
        // Arrange
        Message message = new() { MessageId = 1, HtmlContent = "<p>Updated</p>" };
        Message databaseMessage = new() { MessageId = 1, HtmlContent = "<p>Original</p>" };

        await _context.Messages.AddAsync(databaseMessage);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        IMessageRepository repository = new MessageRepository(_context);

        // Act
        repository.Update(message);

        // Assert
        Message updatedMessage = await _context.Messages.FindAsync(message.MessageId);

        Assert.NotNull(updatedMessage);
        Assert.Equal(message.HtmlContent, updatedMessage.HtmlContent);
    }

    #endregion
}