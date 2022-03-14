using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Messages;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class GetMessageByIdQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public GetMessageByIdQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<Message, MessageResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetMessageByIdHandler_ShouldReturnNull_WhenMessageDoesNotExist()
        {
            // Arrange
            GetMessageByIdQuery request = new() { MessageId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Messages.GetByIdAsync(request.MessageId))
                .ReturnsAsync(null as Message);

            GetMessageByIdQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            MessageResource message = await handler.Handle(request);

            // Assert
            Assert.Null(message);
        }

        [Fact]
        public async Task GetMessageByIdHandler_ShouldReturnMessage_WhenMessageExists()
        {
            // Arrange
            GetMessageByIdQuery request = new() { MessageId = 1 };

            Message expectedMessage = new() { MessageId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Messages.GetByIdAsync(request.MessageId))
                .ReturnsAsync(expectedMessage);

            GetMessageByIdQuery.Handler handler = new(_mapperMock, _unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            MessageResource message = await handler.Handle(request);

            // Assert
            Assert.NotNull(message);
        }
    }
}
