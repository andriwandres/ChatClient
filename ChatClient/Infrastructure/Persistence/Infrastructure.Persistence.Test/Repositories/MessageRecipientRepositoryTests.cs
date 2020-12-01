using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Moq;
using System.Collections.Generic;
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
    }
}
