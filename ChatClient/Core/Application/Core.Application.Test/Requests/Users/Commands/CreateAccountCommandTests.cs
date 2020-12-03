using Core.Application.Database;
using Core.Application.Requests.Users.Commands;
using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Core.Application.Test.Requests.Users.Commands
{
    public class CreateAccountCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICryptoService> _cryptoServiceMock;
        private readonly Mock<IDateProvider> _dateProviderMock;

        public CreateAccountCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cryptoServiceMock = new Mock<ICryptoService>();
            _dateProviderMock = new Mock<IDateProvider>();
            _dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(new DateTime(2020, 1, 1));
        }

        [Fact]
        public async Task CreateAccountCommandHandler_ShouldReturnGeneratedUserId()
        {
            // Arrange
            CreateAccountCommand request = new CreateAccountCommand
            {
                Email = "new@user.account",
                UserName = "newUser",
                Password = "secretPassword"
            };

            byte[] expectedSalt = new byte[16];
            byte[] expectedHash = new byte[16];

            _unitOfWorkMock
                .Setup(m => m.Users.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _cryptoServiceMock
                .Setup(m => m.GenerateSalt())
                .Returns(expectedSalt);

            _cryptoServiceMock
                .Setup(m => m.HashPassword(request.Password, expectedSalt))
                .Returns(expectedHash);

            CreateAccountCommand.Handler handler = 
                new CreateAccountCommand.Handler(_cryptoServiceMock.Object, _unitOfWorkMock.Object, _dateProviderMock.Object);

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWorkMock.Verify(m => m.Users.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()));
            _unitOfWorkMock.Verify(m => m.Recipients.Add(It.IsAny<Recipient>(), It.IsAny<CancellationToken>()));
            _unitOfWorkMock.Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()));
        }
    }
}
