using Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Infrastructure.Shared.Test.Services
{
    public class UserProviderTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public UserProviderTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [Fact]
        public void GetCurrentUserId_ShouldReturnTheCurrentUsersId()
        {
            // Arrange
            const int userId = 1;

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

            UserProvider provider = new UserProvider(_httpContextAccessorMock.Object);

            // Act
            int actualUserId = provider.GetCurrentUserId();

            // Assert
            Assert.Equal(userId, actualUserId);
        }
    }
}
