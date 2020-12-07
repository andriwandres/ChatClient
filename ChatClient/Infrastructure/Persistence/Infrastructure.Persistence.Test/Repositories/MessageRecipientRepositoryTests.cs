using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class MessageRecipientRepositoryTests
    {
        private readonly Mock<IChatContext> _contextMock;

        public MessageRecipientRepositoryTests()
        {
            _contextMock = new Mock<IChatContext>();
        }

        #region Add()

        [Fact]
        public async Task Add_ShouldAddRecipient()
        {
            // Arrange
            MessageRecipient messageRecipient = new MessageRecipient();

            _contextMock.Setup(m => m.MessageRecipients.AddAsync(messageRecipient, It.IsAny<CancellationToken>()));

            MessageRecipientRepository repository = new MessageRecipientRepository(_contextMock.Object);

            // Act
            await repository.Add(messageRecipient);

            // Assert
            _contextMock.Verify(m => m.MessageRecipients.AddAsync(messageRecipient, It.IsAny<CancellationToken>()));
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

            _contextMock.Setup(m => m.MessageRecipients.AddRangeAsync(messageRecipients, It.IsAny<CancellationToken>()));

            MessageRecipientRepository repository = new MessageRecipientRepository(_contextMock.Object);

            // Act
            await repository.AddRange(messageRecipients);

            // Assert
            _contextMock.Verify(m => m.MessageRecipients.AddRangeAsync(messageRecipients, It.IsAny<CancellationToken>()));
        }

        #endregion

        #region GetLatestGroupedByRecipients()

        [Fact]
        public async Task GetLatestGroupedByRecipients_ShouldIncludeRecipientByDirectMessage_WhenTheUserIsTheAuthorInPrivateMessage()
        {
            // Arrange
            const int userId = 1;

            IEnumerable<MessageRecipient> databaseMessageRecipients = new[]
            {
                new MessageRecipient
                {
                    MessageRecipientId = 1,
                    Message = new Message { MessageId = 1, AuthorId = 1, Created = new DateTime (2020, 1, 1) },
                    Recipient = new Recipient { UserId = 2 }
                },
                new MessageRecipient
                {
                    MessageRecipientId = 2,
                    Message = new Message { MessageId = 2, AuthorId = 2, Created = new DateTime (2020, 1, 2) },
                    Recipient = new Recipient { UserId = 3 }
                },
                new MessageRecipient
                {
                    MessageRecipientId = 3,
                    Message = new Message { MessageId = 3, AuthorId = 1, Created = new DateTime (2020, 1, 3) },
                    Recipient = new Recipient { UserId = 3 }
                },
            };

            DbSet<MessageRecipient> dbSetMock = databaseMessageRecipients
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.MessageRecipients)
                .Returns(dbSetMock);

            MessageRecipientRepository repository = new MessageRecipientRepository(_contextMock.Object);

            // Act
            IEnumerable<MessageRecipient> result = await repository
                .GetLatestGroupedByRecipients(userId)
                .ToListAsync();

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

            IEnumerable<MessageRecipient> databaseMessageRecipients = new[]
            {
                new MessageRecipient
                {
                    MessageRecipientId = 1,
                    Message = new Message { MessageId = 1, AuthorId = 2, Created = new DateTime (2020, 1, 1) },
                    Recipient = new Recipient { UserId = 1 } // User as recipient
                },
                new MessageRecipient
                {
                    MessageRecipientId = 2,
                    Message = new Message { MessageId = 2, AuthorId = 3, Created = new DateTime (2020, 1, 2) },
                    Recipient = new Recipient { UserId = 2 }
                },
                new MessageRecipient
                {
                    MessageRecipientId = 3,
                    Message = new Message { MessageId = 3, AuthorId = 3, Created = new DateTime (2020, 1, 3) },
                    Recipient = new Recipient { UserId = 1 } // User as recipient
                },
            };

            DbSet<MessageRecipient> dbSetMock = databaseMessageRecipients
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.MessageRecipients)
                .Returns(dbSetMock);

            MessageRecipientRepository repository = new MessageRecipientRepository(_contextMock.Object);

            // Act
            IEnumerable<MessageRecipient> result = await repository
                .GetLatestGroupedByRecipients(userId)
                .ToListAsync();

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

            IEnumerable<MessageRecipient> databaseMessageRecipients = new[]
            {
                // User is author in message to group 1
                new MessageRecipient
                {
                    MessageRecipientId = 1,
                    Message = new Message { MessageId = 1, AuthorId = 1, Created = new DateTime (2020, 1, 1) },
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
                    Message = new Message { MessageId = 1, AuthorId = 1, Created = new DateTime (2020, 1, 1) },
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
                    Message = new Message { MessageId = 2, AuthorId = 2, Created = new DateTime (2020, 1, 2) },
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
                    Message = new Message { MessageId = 2, AuthorId = 2, Created = new DateTime (2020, 1, 2) },
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

            DbSet<MessageRecipient> dbSetMock = databaseMessageRecipients
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            _contextMock
                .Setup(m => m.MessageRecipients)
                .Returns(dbSetMock);

            MessageRecipientRepository repository = new MessageRecipientRepository(_contextMock.Object);

            // Act
            IEnumerable<MessageRecipient> result = await repository
                .GetLatestGroupedByRecipients(userId)
                .ToListAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());

            Assert.Equal(4, result.ElementAt(0).MessageRecipientId);
            Assert.Equal(1, result.ElementAt(1).MessageRecipientId);
        }

        #endregion
    }
}
