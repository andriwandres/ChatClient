using AutoMapper;
using Core.Application.Database;
using Core.Application.Requests.Session.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Resources.Users;
using Moq;
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

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<User, AuthenticatedUserResource>();
            });

            _mapperMock = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task LoginCommandHandler_ShouldReturnNull_WhenUserNameOrEmailAreInvalid()
        {
            // Arrange
            LoginCommand request = new()
            {
                UserNameOrEmail = "invalid.username@or.email"
            };

            _unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            LoginCommand.Handler handler = new(_unitOfWorkMock.Object, null, null);

            // Act
            AuthenticatedUserResource user = await handler.Handle(request);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task LoginCommandHandler_ShouldReturnNull_WhenPasswordIsInvalid()
        {
            // Arrange
            LoginCommand request = new()
            {
                UserNameOrEmail = "my@email.com",
                Password = "wrongpassword"
            };

            _unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _cryptoServiceMock
                .Setup(m => m.VerifyPassword(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns(false);

            LoginCommand.Handler handler = new(_unitOfWorkMock.Object, null, _cryptoServiceMock.Object);

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

            User expectedUser = new() { UserId = 1 };

            LoginCommand request = new()
            {
                UserNameOrEmail = "my@email.com",
                Password = "correctpassword"
            };

            _unitOfWorkMock
                .Setup(m => m.Users.GetByUserNameOrEmail(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            _cryptoServiceMock
                .Setup(m => m.VerifyPassword(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns(true);

            _cryptoServiceMock
                .Setup(m => m.GenerateToken(It.IsAny<User>()))
                .Returns(expectedToken);

            LoginCommand.Handler handler = new(_unitOfWorkMock.Object, _mapperMock, _cryptoServiceMock.Object);

            // Act
            AuthenticatedUserResource user = await handler.Handle(request);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, user.UserId);
            Assert.Equal(expectedToken, user.Token);
        }
    }
}
