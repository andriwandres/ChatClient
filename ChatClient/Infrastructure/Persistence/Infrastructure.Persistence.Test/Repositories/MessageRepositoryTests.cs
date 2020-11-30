using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class MessageRepositoryTests
    {
        private readonly Mock<IChatContext> _contextMock;

        public MessageRepositoryTests()
        {
            _contextMock = new Mock<IChatContext>();
        }

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotMatch()
        {
            // Arrange
            const int messageId = 5204;

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1 },
                new Message { MessageId = 2 },
                new Message { MessageId = 3 },
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

            // Act
            Message message = await repository
                .GetById(messageId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public async Task GetById_ShouldReturnMessage_WhenIdMatches()
        {
            // Arrange
            const int messageId = 2;

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1 },
                new Message { MessageId = 2 },
                new Message { MessageId = 3 },
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

            // Act
            Message message = await repository
                .GetById(messageId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.NotNull(message);
            Assert.Equal(messageId, message.MessageId);
        }

        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenMessageExists()
        {
            // Arrange
            const int messageId = 1;

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1 },
                new Message { MessageId = 2 },
                new Message { MessageId = 3 },
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

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

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1 },
                new Message { MessageId = 2 },
                new Message { MessageId = 3 },
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

            // Act
            bool exists = await repository.Exists(messageId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task CanAccess_ShouldReturnTrue_WhenTheUserIsTheAuthorOfTheMessage()
        {
            // Arrange
            const int userId = 1;
            const int messageId = 1;

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1, AuthorId = 1, MessageRecipients = new List<MessageRecipient>
                {
                    new MessageRecipient { Recipient = new Recipient { UserId = 2 }},
                    new MessageRecipient { Recipient = new Recipient { UserId = 3 }}
                }},
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

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

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1, AuthorId = 2, MessageRecipients = new List<MessageRecipient>
                {
                    new MessageRecipient { Recipient = new Recipient { UserId = 1 }},
                    new MessageRecipient { Recipient = new Recipient { UserId = 3 }}
                }},
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

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

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1, AuthorId = 2, MessageRecipients = new List<MessageRecipient>
                {
                    new MessageRecipient { Recipient = new Recipient { GroupMembership = new GroupMembership { UserId = 3 }}},
                    new MessageRecipient { Recipient = new Recipient { GroupMembership = new GroupMembership { UserId = 1 }}},
                    new MessageRecipient { Recipient = new Recipient { GroupMembership = new GroupMembership { UserId = 4 }}}
                }},
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

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

            DbSet<Message> expectedMessages = new[]
            {
                new Message { MessageId = 1, AuthorId = 2, MessageRecipients = new List<MessageRecipient>
                {
                    new MessageRecipient { Recipient = new Recipient { UserId = 3 }},
                    new MessageRecipient { Recipient = new Recipient { UserId = 4 }}
                }},
            }
            .AsQueryable()
            .BuildMockDbSet()
            .Object;

            _contextMock
                .Setup(m => m.Messages)
                .Returns(expectedMessages);

            MessageRepository repository = new MessageRepository(_contextMock.Object);

            // Act
            bool canAccess = await repository.CanAccess(messageId, userId);

            // Assert
            Assert.False(canAccess);
        }
    }
}
