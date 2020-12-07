using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Recipients.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Recipients;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Core.Application.Test.Requests.Recipients.Queries
{
    public class GetOwnRecipientsQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public GetOwnRecipientsQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);
            
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<MessageRecipient, RecipientResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetOwnRecipientsQueryHandler_ShouldReturnRelevantRecipients()
        {
            // Arrange
            IEnumerable<MessageRecipient> databaseRecipients = new[]
            {
                new MessageRecipient {MessageRecipientId = 1, RecipientId = 1},
                new MessageRecipient {MessageRecipientId = 2, RecipientId = 2},
            };

            IQueryable<MessageRecipient> queryableMock = databaseRecipients
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.MessageRecipients.GetLatestGroupedByRecipients(It.IsAny<int>()))
                .Returns(queryableMock);

            GetOwnRecipientsQuery.Handler handler = new GetOwnRecipientsQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object, _mapperMock);
            
            // Act
            IEnumerable<RecipientResource> recipients = await handler.Handle(new GetOwnRecipientsQuery());

            // Assert
            Assert.Equal(2, recipients.Count());
        }
    }
}
