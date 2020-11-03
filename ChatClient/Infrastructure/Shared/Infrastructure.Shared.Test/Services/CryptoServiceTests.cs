using System;
using System.Collections.Generic;
using System.Text;
using Core.Application.Services;
using Core.Domain.Entities;
using Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Xunit;

namespace Infrastructure.Shared.Test.Services
{
    public class CryptoServiceTests
    {
        [Fact]
        public void GenerateToken_ShouldGenerateSameToken_GivenTheSamePayload()
        {
            // Arrange
            const string secret = "0000_1111_2222_3333_4444_6666_7777_8888_9999";
            DateTime expectedDate = DateTime.UtcNow;

            User user = new User
            {
                Email = "test@test.test",
                UserName = "testusername",
                DisplayId = "test7357"
            };

            Mock<IDateProvider> dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(expectedDate);

            ICryptoService cryptoService = new CryptoService(dateProviderMock.Object);

            // Act
            string firstToken = cryptoService.GenerateToken(user, secret);
            string secondToken = cryptoService.GenerateToken(user, secret);

            // Assert
            Assert.NotNull(firstToken);
            Assert.NotNull(secondToken);

            Assert.Equal(firstToken, secondToken);
        }

        [Fact]
        public void GenerateToken_ShouldNotGenerateSameToken_GivenDifferentPayloads()
        {
            // Arrange
            const string secret = "super_secret_string";

            DateTime expectedDate = DateTime.UtcNow;

            User user = new User
            {
                Email = "test@test.test",
                UserName = "testusername",
                DisplayId = "test7357"
            };

            Mock<IDateProvider> dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock
                .Setup(m => m.UtcNow())
                .Returns(expectedDate);

            ICryptoService cryptoService = new CryptoService(dateProviderMock.Object);

            // Act
            string firstToken = cryptoService.GenerateToken(user, secret);

            user.Email = "different@email.address";

            string secondToken = cryptoService.GenerateToken(user, secret);

            // Assert
            Assert.NotNull(firstToken);
            Assert.NotNull(secondToken);

            Assert.NotEqual(firstToken, secondToken);
        }

        [Fact]
        public void GenerateSalt_ShouldNotGenerateSameSaltTwice()
        {
            // Arrange
            ICryptoService cryptoService = new CryptoService(null);

            // Act
            byte[] firstSalt = cryptoService.GenerateSalt();
            byte[] secondSalt = cryptoService.GenerateSalt();

            // Assert
            Assert.NotNull(firstSalt);
            Assert.NotNull(secondSalt);

            Assert.NotEqual(firstSalt, secondSalt);
        }

        [Fact]
        public void HashPassword_ShouldGenerateSamePassword_GivenTheSamePayload()
        {
            // Arrange
            const string password = "str0ngP4ssw0rd!";
            byte[] salt = new byte[16];

            ICryptoService cryptoService = new CryptoService(null);

            // Act
            byte[] firstHash = cryptoService.HashPassword(password, salt);
            byte[] secondHash = cryptoService.HashPassword(password, salt);

            // Assert
            Assert.NotNull(firstHash);
            Assert.NotNull(secondHash);

            Assert.Equal(firstHash, secondHash);
        }

        [Fact]
        public void HashPassword_ShouldNotGenerateSamePassword_GivenDifferentPayloads()
        {
            // Arrange
            const string firstPassword = "str0ngP4ssw0rd!";
            const string secondPassword = "!d1ff3rentP4ssw0rd";

            byte[] salt = new byte[16];

            ICryptoService cryptoService = new CryptoService(null);

            // Act
            byte[] firstHash = cryptoService.HashPassword(firstPassword, salt);
            byte[] secondHash = cryptoService.HashPassword(secondPassword, salt);

            string s = Convert.ToBase64String(firstHash);

            // Assert
            Assert.NotNull(firstHash);
            Assert.NotNull(secondHash);

            Assert.NotEqual(firstHash, secondHash);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_GivenTheSamePayload()
        {
            // Arrange
            const string password = "str0ngP4ssw0rd!";
            byte[] salt = new byte[16];
            byte[] hash = Convert.FromBase64String("2VxiBg7W1G3EQVlOucLRTQKLe0sTEBXt6QZVlfkfokQ=");

            ICryptoService cryptoService = new CryptoService(null);

            // Act
            bool result = cryptoService.VerifyPassword(hash, salt, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_GivenDifferentPayloads()
        {
            // Arrange
            const string password = "wr0ngP4ssw0rd!";
            byte[] salt = new byte[16];
            byte[] hash = Convert.FromBase64String("2VxiBg7W1G3EQVlOucLRTQKLe0sTEBXt6QZVlfkfokQ=");

            ICryptoService cryptoService = new CryptoService(null);

            // Act
            bool result = cryptoService.VerifyPassword(hash, salt, password);

            // Assert
            Assert.False(result);
        }
    }
}
