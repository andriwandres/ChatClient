using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using MockQueryable.Moq;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class IsAuthorOfMessageQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;

        public IsAuthorOfMessageQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);
        }

        [Fact]
        public async Task IsAuthorOfMessageQuery_ShouldReturnTrue_WhenUserIsAuthor()
        {
            // Arrange
            IsAuthorOfMessageQuery request = new IsAuthorOfMessageQuery { MessageId = 1 };

            IQueryable<Message> databaseMessage = new[]
            {
                new Message { MessageId = 1, AuthorId = 1 }
            }
            .AsQueryable()
            .BuildMock()
            .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(databaseMessage);

            IsAuthorOfMessageQuery.Handler handler = new IsAuthorOfMessageQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            bool isAuthor = await handler.Handle(request);

            // Assert
            Assert.True(isAuthor);
        }

        [Fact]
        public async Task IsAuthorOfMessageQuery_ShouldReturnFalse_WhenUserIsNotAuthor()
        {
            // Arrange
            IsAuthorOfMessageQuery request = new IsAuthorOfMessageQuery { MessageId = 1 };

            IQueryable<Message> databaseMessage = new[]
                {
                    new Message { MessageId = 1, AuthorId = 2 }
                }
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.Messages.GetById(request.MessageId))
                .Returns(databaseMessage);

            IsAuthorOfMessageQuery.Handler handler = new IsAuthorOfMessageQuery.Handler(_unitOfWorkMock.Object, _userProviderMock.Object);

            // Act
            bool isAuthor = await handler.Handle(request);

            // Assert
            Assert.False(isAuthor);
        }
    }
}
