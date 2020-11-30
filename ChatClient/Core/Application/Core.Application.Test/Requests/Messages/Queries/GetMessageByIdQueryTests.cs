using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Domain.Entities;
using Core.Domain.Resources.Messages;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class GetMessageByIdQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetMessageByIdQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Message, MessageResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetMessageByIdHandler_ShouldReturnNull_WhenMessageDoesNotExist()
        {
            // Arrange
            GetMessageByIdQuery request = new GetMessageByIdQuery { MessageId = 1 };

            IQueryable<Message> expectedMessage = Enumerable
                .Empty<Message>()
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(expectedMessage);

            GetMessageByIdQuery.Handler handler = new GetMessageByIdQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            MessageResource message = await handler.Handle(request);

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public async Task GetMessageByIdHandler_ShouldReturnMessage_WhenMessageExists()
        {
            // Arrange
            GetMessageByIdQuery request = new GetMessageByIdQuery { MessageId = 1 };

            IQueryable<Message> expectedMessage = new[]
            {
                new Message { MessageId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(expectedMessage);

            GetMessageByIdQuery.Handler handler = new GetMessageByIdQuery.Handler(_mapperMock, _unitOfWorkMock.Object);

            // Act
            MessageResource message = await handler.Handle(request);

            // Assert
            Assert.NotNull(message);
        }
    }
}
