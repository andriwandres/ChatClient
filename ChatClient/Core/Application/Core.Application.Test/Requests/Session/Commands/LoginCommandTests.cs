using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Session.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Session.Commands
{
    public class LoginCommandTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICryptoService> _cryptoServiceMock;

        public LoginCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cryptoServiceMock = new Mock<ICryptoService>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, AuthenticatedUserResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task LoginCommandHandler_ShouldReturnNull_WhenUserNameOrEmailAreInvalid()
        {
            // Arrange
            IEnumerable<User> expectedUser = new User[] { };

            LoginCommand request = new LoginCommand
            {
                UserNameOrEmail = "invalid.username@or.email"
            };

            Mock<IQueryable<User>> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock();

            _unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .Returns(userQueryableMock.Object);

            LoginCommand.Handler handler = new LoginCommand.Handler(_unitOfWorkMock.Object, null, null);

            // Act
            AuthenticatedUserResource user = await handler.Handle(request);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task LoginCommandHandler_ShouldReturnNull_WhenPasswordIsInvalid()
        {
            // Arrange
            IEnumerable<User> expectedUser = new [] { new User() };

            LoginCommand request = new LoginCommand
            {
                UserNameOrEmail = "my@email.com",
                Password = "wrongpassword"
            };

            IQueryable<User> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .Returns(userQueryableMock);

            _cryptoServiceMock
                .Setup(m => m.VerifyPassword(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns(false);

            LoginCommand.Handler handler = new LoginCommand.Handler(_unitOfWorkMock.Object, null, _cryptoServiceMock.Object);

            // Act
            AuthenticatedUserResource user = await handler.Handle(request);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task LoginCommandHandler_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            // Arrange
            const string expectedToken = "some.access.token";
            
            IEnumerable<User> expectedUser = new[]
            {
                new User { UserId = 1 }
            };

            LoginCommand request = new LoginCommand
            {
                UserNameOrEmail = "my@email.com",
                Password = "correctpassword"
            };

            IQueryable<User> userQueryableMock = expectedUser
                .AsQueryable()
                .BuildMock()
                .Object;

            _unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .Returns(userQueryableMock);

            _cryptoServiceMock
                .Setup(m => m.VerifyPassword(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns(true);

            _cryptoServiceMock
                .Setup(m => m.GenerateToken(It.IsAny<User>()))
                .Returns(expectedToken);

            LoginCommand.Handler handler = new LoginCommand.Handler(_unitOfWorkMock.Object, _mapperMock, _cryptoServiceMock.Object);

            // Act
            AuthenticatedUserResource user = await handler.Handle(request);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, user.UserId);
            Assert.Equal(expectedToken, user.Token);
        }
    }
}
