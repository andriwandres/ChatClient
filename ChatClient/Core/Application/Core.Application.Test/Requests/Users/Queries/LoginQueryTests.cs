using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Users.Queries;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Queries
{
    public class LoginQueryTests
    {
        [Fact]
        public async Task LoginQueryHandler_ShouldReturnNull_WhenUserNameOrEmailAreInvalid()
        {
            // Arrange
            IEnumerable<User> expectedUser = new User[] { };

            LoginQuery request = new LoginQuery
            {
                UserNameOrEmail = "invalid.username@or.email"
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock();

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .Returns(userQueryableMock.Object);

            LoginQuery.LoginQueryHandler handler = new LoginQuery.LoginQueryHandler(unitOfWorkMock.Object, null, null);

            // Act
            AuthenticatedUser user = await handler.Handle(request);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task LoginQueryHandler_ShouldReturnNull_WhenPasswordIsInvalid()
        {
            // Arrange
            IEnumerable<User> expectedUser = new [] { new User() };

            LoginQuery request = new LoginQuery
            {
                UserNameOrEmail = "my@email.com",
                Password = "wrongpassword"
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock();

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .Returns(userQueryableMock.Object);

            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            cryptoServiceMock
                .Setup(m => m.VerifyPassword(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns(false);

            LoginQuery.LoginQueryHandler handler = new LoginQuery.LoginQueryHandler(unitOfWorkMock.Object, null, cryptoServiceMock.Object);

            // Act
            AuthenticatedUser user = await handler.Handle(request);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task LoginQueryHandler_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            // Arrange
            const string expectedToken = "some.access.token";
            
            IEnumerable<User> expectedUser = new[]
            {
                new User { UserId = 1 }
            };

            LoginQuery request = new LoginQuery
            {
                UserNameOrEmail = "my@email.com",
                Password = "correctpassword"
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock();

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .Returns(userQueryableMock.Object);

            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            cryptoServiceMock
                .Setup(m => m.VerifyPassword(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns(true);

            cryptoServiceMock
                .Setup(m => m.GenerateToken(It.IsAny<User>()))
                .Returns(expectedToken);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, AuthenticatedUser>();
            });

            IMapper mapperMock = mapperConfiguration.CreateMapper();

            LoginQuery.LoginQueryHandler handler = new LoginQuery.LoginQueryHandler(unitOfWorkMock.Object, mapperMock, cryptoServiceMock.Object);

            // Act
            AuthenticatedUser user = await handler.Handle(request);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, user.UserId);
            Assert.Equal(expectedToken, user.Token);
        }
    }
}
