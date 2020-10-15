using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Repositories;
using ChatClient.Tests.TestUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChatClient.Tests.RepositoryTests
{
    public class MessageRepositoryTests : RepositoryTestsBase
    {
        private readonly IMessageRepository _systemUnderTest;

        public MessageRepositoryTests()
        {
            // Insert test data
            DataContainer data = new DataContainer
            {
                Messages = new Message[]
                {
                    new Message { MessageId = 1, TextContent = "Test" }
                }
            };

            SeedDatabase(data);

            // Initialize system under test
            _systemUnderTest = new MessageRepository(Context);
        }

        [Fact]
        public async Task GetMessageById_ShouldReturnMessage()
        {
            // Arrange
            const int messageId = 1;

            // Act
            Message message = await _systemUnderTest.GetMessageById(messageId);

            // Assert
            Assert.NotNull(message);
            Assert.Equal(messageId, message.MessageId);
        }

        [Fact]
        public async Task GetMessageById_ShouldReturnNull()
        {
            // Arrange
            const int invalidMessageId = 9999;

            // Act
            Message message = await _systemUnderTest.GetMessageById(invalidMessageId);

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public async Task AddMessage_ShouldAddMessage()
        {
            // Arrange
            Message message = new Message();

            // Act
            await _systemUnderTest.AddMessage(message);

            // Assert
            Assert.Equal(2, message.MessageId);
        }

        [Fact]
        public async Task EditMessage_ShouldUpdateMessage()
        {
            // Arrange
            Message message = await Context.Messages.FirstOrDefaultAsync(m => m.MessageId == 1);

            message.TextContent = "Updated Test";

            // Act
            _systemUnderTest.EditMessage(message);

            // Assert
            Message updatedMessage = await Context.Messages.FirstOrDefaultAsync(m => m.MessageId == 1);

            Assert.NotNull(updatedMessage);
            Assert.Equal("Updated Test", updatedMessage.TextContent);
        }
    }
}
