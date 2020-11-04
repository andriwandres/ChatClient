using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
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
        [Fact]
        public async Task AuthenticateQueryHandler_ShouldReturnUser_WhenTokenIsValid()
        {
            // Arrange
            const string expectedToken = "some.access.token";

            Claim expectedNameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, "1");

            IHeaderDictionary headers = new HeaderDictionary
            {
                { "Authorization", expectedToken }
            };

            IEnumerable<User> expectedUser = new []
            {
                new User { UserId = 1 }
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser.AsQueryable().BuildMock();

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            httpContextAccessorMock
                .Setup(m => m.HttpContext.Request.Headers)
                .Returns(headers);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetById(It.IsAny<int>()))
                .Returns(userQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, AuthenticatedUser>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            AuthenticateQuery.AuthenticateQueryHandler handler = 
                new AuthenticateQuery.AuthenticateQueryHandler(unitOfWorkMock.Object, mapperMock, httpContextAccessorMock.Object);

            // Act
            AuthenticatedUser user = await handler.Handle(new AuthenticateQuery());

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

            Mock<IQueryable<User>> userQueryableMock = expectedUser.AsQueryable().BuildMock();

            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock
                .Setup(m => m.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(expectedNameIdentifierClaim);

            httpContextAccessorMock
                .Setup(m => m.HttpContext.Request.Headers)
                .Returns(headers);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetById(It.IsAny<int>()))
                .Returns(userQueryableMock.Object);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, AuthenticatedUser>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            AuthenticateQuery.AuthenticateQueryHandler handler =
                new AuthenticateQuery.AuthenticateQueryHandler(unitOfWorkMock.Object, mapperMock, httpContextAccessorMock.Object);

            // Act
            AuthenticatedUser user = await handler.Handle(new AuthenticateQuery());

            // Assert
            Assert.Null(user);
        }
    }
}

