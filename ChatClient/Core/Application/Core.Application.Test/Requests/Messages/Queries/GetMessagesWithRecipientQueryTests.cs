using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Messages;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class GetMessagesWithRecipientQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public GetMessagesWithRecipientQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<MessageRecipient, ChatMessageResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetMessagesWithRecipientQueryHandler_ShouldReturnMessages()
        {
            // Arrange
            GetMessagesWithRecipientQuery request = new GetMessagesWithRecipientQuery { RecipientId = 1 };

            IEnumerable<MessageRecipient> expectedMessageRecipients = new[]
            {
                new MessageRecipient {MessageRecipientId = 1},
                new MessageRecipient {MessageRecipientId = 2},
            };

            IQueryable<MessageRecipient> queryableMock = expectedMessageRecipients
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.GetMessagesWithRecipient(1, request.RecipientId))
                .Returns(queryableMock);

            GetMessagesWithRecipientQuery.Handler handler = new GetMessagesWithRecipientQuery.Handler(_mapperMock, _unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            IEnumerable<ChatMessageResource> result = await handler.Handle(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
