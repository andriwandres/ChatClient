using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
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

            IEnumerable<User> expectedUser = new []
            {
                new User { UserId = 1 }
            };

            IQueryable<User> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock()
                .Object;

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.Request.Headers)
                .Returns(headers);

            _unitOfWorkMock
                .Setup(m => m.Users.GetById(It.IsAny<int>()))
                .Returns(userQueryableMock);

            AuthenticateQuery.Handler handler = 
                new AuthenticateQuery.Handler(_unitOfWorkMock.Object, _mapperMock, _httpContextAccessorMock.Object, _userProviderMock.Object);

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

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "8979");

            IHeaderDictionary headers = new HeaderDictionary
            {
                { "Authorization", expectedToken }
            };

            IEnumerable<User> expectedUser = Enumerable.Empty<User>();

            IQueryable<User> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock()
                .Object;

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            _httpContextAccessorMock
                .Setup(m => m.HttpContext.Request.Headers)
                .Returns(headers);

            _unitOfWorkMock
                .Setup(m => m.Users.GetById(It.IsAny<int>()))
                .Returns(userQueryableMock);

            AuthenticateQuery.Handler handler =
                new AuthenticateQuery.Handler(_unitOfWorkMock.Object, _mapperMock, _httpContextAccessorMock.Object, _userProviderMock.Object);

            // Act
            AuthenticatedUserResource user = await handler.Handle(new AuthenticateQuery());

            // Assert
            Assert.Null(user);
        }
    }
}

