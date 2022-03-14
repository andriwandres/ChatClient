using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class AuthenticateQueryTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserProvider> _userProviderMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public AuthenticateQueryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userProviderMock = new Mock<IUserProvider>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _userProviderMock
                .Setup(m => m.GetCurrentUserId())
                .Returns(1);

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<User, AuthenticatedUserResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task AuthenticateQueryHandler_ShouldReturnUser_WhenTokenIsValid()
        {
            // Arrange
            const string expectedToken = "some.access.token";

            IHeaderDictionary headers = new HeaderDictionary
            {
                { "Authorization", expectedToken }
            };

            User expectedUser = new() { UserId = 1 };

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.Request.Headers)
                .Returns(headers);

            _unitOfWorkMock
                .Setup(m => m.Users.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedUser);

            AuthenticateQuery.Handler handler = 
                new(_unitOfWorkMock.Object, _mapperMock, _httpContextAccessorMock.Object, _userProviderMock.Object);

            // Act
            AuthenticatedUserResource user = await handler.Handle(new AuthenticateQuery());

            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, user.UserId);
            Assert.Equal(expectedToken, user.Token);
        }

        [Fact]
        public async Task AuthenticateQueryHandler_ShouldReturnNull_WhenNameIdentifierClaimIsInvalid()
        {
            // Arrange
            const string expectedToken = "some.access.token";

            Claim expectedNameIdentifierClaim = new(ClaimTypes.NameIdentifier, "8979");

            IHeaderDictionary headers = new HeaderDictionary
            {
                { "Authorization", expectedToken }
            };

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.Request.Headers)
                .Returns(headers);

            _unitOfWorkMock
                .Setup(m => m.Users.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as User);

            AuthenticateQuery.Handler handler =
                new(_unitOfWorkMock.Object, _mapperMock, _httpContextAccessorMock.Object, _userProviderMock.Object);

            // Act
            AuthenticatedUserResource user = await handler.Handle(new AuthenticateQuery());

            // Assert
            Assert.Null(user);
        }
    }
}

