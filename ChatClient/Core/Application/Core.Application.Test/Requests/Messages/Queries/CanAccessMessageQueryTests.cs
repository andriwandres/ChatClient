using Core.Application.Database;
using Core.Application.Requests.Messages.Queries;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Messages.Queries
{
    public class CanAccessMessageQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public CanAccessMessageQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            Claim nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, 1.ToString());

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(nameIdentifierClaim);
        }

        [Fact]
        public async Task CanAccessMessageQueryHandler_ShouldReturnTrue_WhenUserCanAccessMessage()
        {
            // Arrange
            CanAccessMessageQuery request = new CanAccessMessageQuery {MessageId = 1};

            _unitOfWorkMock
                .Setup(m => m.Messages.CanAccess(request.MessageId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            CanAccessMessageQuery.Handler handler = new CanAccessMessageQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool canAccess = await handler.Handle(request);

            // Assert
            Assert.True(canAccess);
        }

        [Fact]
        public async Task CanAccessMessageQueryHandler_ShouldReturnFalse_WhenUserIsNotAllowedToAccessMessage()
        {
            // Arrange
            CanAccessMessageQuery request = new CanAccessMessageQuery { MessageId = 1 };

            _unitOfWorkMock
                .Setup(m => m.Messages.CanAccess(request.MessageId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            CanAccessMessageQuery.Handler handler = new CanAccessMessageQuery.Handler(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);

            // Act
            bool canAccess = await handler.Handle(request);

            // Assert
            Assert.False(canAccess);
        }
    }
}
