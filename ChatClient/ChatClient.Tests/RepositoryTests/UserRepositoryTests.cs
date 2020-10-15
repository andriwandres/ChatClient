using ChatClient.Core.Models.Domain;
using ChatClient.Core.Repositories;
using ChatClient.Data.Repositories;
using ChatClient.Tests.TestUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChatClient.Tests.RepositoryTests
{
    public class UserRepositoryTests : RepositoryTestsBase
    {
        private readonly IUserRepository _systemUnderTest;

        public UserRepositoryTests()
        {
            // Add required data for testing
            DataContainer data = new DataContainer
            {
                Users = new User[]
                {
                    new User
                    {
                        UserId = 1,
                        UserCode = "A1B2C3",
                        Email = "test@test.com"
                    }
                }
            };

            SeedDatabase(data);

            // Initialize system under test
            _systemUnderTest = new UserRepository(Context);
        }

        [Fact]
        public async Task GetUserByCode_ShouldReturnUser()
        {
            // Arrange
            const string code = "A1B2C3";

            // Act
            User user = await _systemUnderTest.GetUserByCode(code);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(code, user.UserCode);
        }

        [Fact]
        public async Task GetUserByCode_ShouldReturnNull()
        {
            // Arrange
            const string code = "Z9Y8X7";

            // Act
            User user = await _systemUnderTest.GetUserByCode(code);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnUser()
        {
            // Arrange
            const string email = "test@test.com";

            // Act
            User user = await _systemUnderTest.GetUserByEmail(email);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnNull()
        {
            // Arrange
            const string email = "not@existing.com";

            // Act
            User user = await _systemUnderTest.GetUserByEmail(email);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser()
        {
            // Arrange
            const int userId = 1;

            // Act
            User user = await _systemUnderTest.GetUserById(userId);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(userId, user.UserId);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull()
        {
            // Arrange
            const int userId = 9999999;

            // Act
            User user = await _systemUnderTest.GetUserById(userId);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task IsEmailTaken_ShouldReturnTrue()
        {
            // Arrange
            const string email = "test@test.com";

            // Act
            bool isTaken = await _systemUnderTest.IsEmailTaken(email);

            // Assert
            Assert.True(isTaken);
        }

        [Fact]
        public async Task IsEmailTaken_ShouldReturnFalse()
        {
            // Arrange
            const string email = "not@existing.com";

            // Act
            bool isTaken = await _systemUnderTest.IsEmailTaken(email);

            // Assert
            Assert.False(isTaken);
        }

        [Fact]
        public async Task UserCodeExists_ShouldReturnTrue()
        {
            // Arrange
            const string code = "A1B2C3";

            // Act
            bool exists = await _systemUnderTest.UserCodeExists(code);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserCodeExists_ShouldReturnFalse()
        {
            // Arrange
            const string code = "Z9Y8X7";

            // Act
            bool exists = await _systemUnderTest.UserCodeExists(code);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task AddUser_ShouldAddUser()
        {
            // Arrange
            User user = new User
            {
                Email = "additional@user.com",
                UserCode = "E4F5G6",
                DisplayName = "AdditionalUser",
                PasswordHash = Convert.FromBase64String("wlD7izxOXkee+pDfrKJD037DtDTz2Yyi0E5kLRwC9W8="),
                PasswordSalt = Convert.FromBase64String("oC8ig9fbCMG63d7LCYL8qA=="),
                CreatedAt = DateTime.Now
            };

            // Act
            await _systemUnderTest.AddUser(user);

            // Assert
            Assert.NotEqual(0, user.UserId);
        }
    }
}
