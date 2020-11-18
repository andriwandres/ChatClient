using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public void GetById_ShouldReturnUser_WhenUserIdMatches()
        {
            // Arrange
            const int expectedUserId = 1;

            IEnumerable<User> users = new []
            {
                new User { UserId = 1 },
                new User { UserId = 2 },
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetById(expectedUserId);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Single(userQueryable);

            User user = userQueryable.SingleOrDefault();

            Assert.NotNull(user);
            Assert.Equal(expectedUserId, user.UserId);
        }

        [Fact]
        public void GetById_ShouldReturnEmptyQueryable_WhenUserIdIsInvalid()
        {
            // Arrange
            IEnumerable<User> users = new[]
            {
                new User { UserId = 1 },
                new User { UserId = 2 },
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetById(3);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Empty(userQueryable);
        }

        [Fact]
        public void GetByUserNameOrEmail_ShouldReturnUser_WhenEmailMatches()
        {
            // Arrange
            const string email = "user1@test.com";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, Email = "user1@test.com", UserName = "User1" },
                new User { UserId = 2, Email = "user2@test.com", UserName = "User2" }
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetByUserNameOrEmail(email);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Single(userQueryable);

            User user = userQueryable.SingleOrDefault();

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void GetByUserNameOrEmail_ShouldReturnUser_WhenEmailMatches_WithDifferentCase()
        {
            // Arrange
            const string email = "USER1@TEST.COM";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, Email = "user1@test.com", UserName = "User1" },
                new User { UserId = 2, Email = "user2@test.com", UserName = "User2" }
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetByUserNameOrEmail(email);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Single(userQueryable);

            User user = userQueryable.SingleOrDefault();

            Assert.NotNull(user);
            Assert.Equal(email.ToLower(), user.Email.ToLower());
        }

        [Fact]
        public void GetByUserNameOrEmail_ShouldReturnUser_WhenUserNameMatches()
        {
            // Arrange
            const string userName = "User1";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, Email = "user1@test.com", UserName = "User1" },
                new User { UserId = 2, Email = "user2@test.com", UserName = "User2" }
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetByUserNameOrEmail(userName);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Single(userQueryable);

            User user = userQueryable.SingleOrDefault();

            Assert.NotNull(user);
            Assert.Equal(userName, user.UserName);
        }

        [Fact]
        public void GetByUserNameOrEmail_ShouldReturnUser_WhenUserNameMatches_WithDifferentCase()
        {
            // Arrange
            const string userName = "USER1";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, Email = "user1@test.com", UserName = "User1" },
                new User { UserId = 2, Email = "user2@test.com", UserName = "User2" }
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetByUserNameOrEmail(userName);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Single(userQueryable);

            User user = userQueryable.SingleOrDefault();

            Assert.NotNull(user);
            Assert.Equal(userName.ToLower(), user.UserName.ToLower());
        }

        [Fact]
        public void GetByUserNameOrEmail_ShouldReturnEmptyQueryable_WhenUserNameOrEmailAreInvalid()
        {
            // Arrange
            const string input = "xxx_invalid_xxx";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, Email = "user1@test.com", UserName = "User1" },
                new User { UserId = 2, Email = "user2@test.com", UserName = "User2" }
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            IQueryable<User> userQueryable = userRepository.GetByUserNameOrEmail(input);

            // Assert
            Assert.NotNull(userQueryable);
            Assert.Empty(userQueryable);
        }

        [Fact]
        public async Task EmailExists_ShouldReturnTrue_WhenEmailExists()
        {
            // Arrange
            const string email = "test@test.test";

            IEnumerable<User> users = new []
            {
                new User { UserId = 1, Email = "a@b.c" },
                new User { UserId = 2, Email = "test@test.test" },
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await userRepository.EmailExists(email);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task EmailExists_ShouldReturnFalse_WhenEmailDoesNotExist()
        {
            // Arrange
            const string email = "invalid@email.address";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, Email = "a@b.c" },
                new User { UserId = 2, Email = "test@test.test" },
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await userRepository.EmailExists(email);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnTrue_WhenUserNameExists()
        {
            // Arrange
            const string userName = "myUserName";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, UserName = "otherUserName" },
                new User { UserId = 2, UserName = "myUserName" },
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await userRepository.UserNameExists(userName);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserNameExists_ShouldReturnFalse_WhenUserNameDoesNotExist()
        {
            // Arrange
            const string userName = "notExistingUserName";

            IEnumerable<User> users = new[]
            {
                new User { UserId = 1, UserName = "otherUserName" },
                new User { UserId = 2, UserName = "myUserName" },
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await userRepository.UserNameExists(userName);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task Add_ShouldAddUser()
        {
            // Arrange
            User user = new User
            {
                UserName = "myUsername",
                Email = "my@email.address"
            };

            Mock<DbSet<User>> userDbSetMock = Enumerable
                .Empty<User>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            IUserRepository userRepository = new UserRepository(contextMock.Object);

            // Act
            await userRepository.Add(user);

            // Assert
            contextMock.Verify(m => m.Users.AddAsync(user, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task UserNameOrEmailExists_ShouldReturnFalse_WhenNeitherUserNameNorEmailMatch()
        {
            // Arrange
            const string userName = "myUserName";
            const string email = "my@email.address";

            IEnumerable<User> users = new[]
            {
                new User {Email = "Something@else.com", UserName = "Somethingelse"},
                new User {Email = "Something@else.again", UserName = "Somethingelseagain"},
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await repository.UserNameOrEmailExists(userName, email);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task UserNameOrEmailExists_ShouldReturnTrue_WhenJustUserNameMatches()
        {
            // Arrange
            const string userName = "myUserName";
            const string email = "my@email.address";

            IEnumerable<User> users = new[]
            {
                new User {Email = "Something@else.com", UserName = "MyUSERNAME"},
                new User {Email = "Something@else.again", UserName = "Somethingelseagain"},
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await repository.UserNameOrEmailExists(userName, email);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserNameOrEmailExists_ShouldReturnTrue_WhenJustEmailMatches()
        {
            // Arrange
            const string userName = "myUserName";
            const string email = "my@email.address";

            IEnumerable<User> users = new[]
            {
                new User {Email = "Something@else.com", UserName = "Somethingelse"},
                new User {Email = "my@EMAIL.address", UserName = "Somethingelseagain"},
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await repository.UserNameOrEmailExists(userName, email);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserNameOrEmailExists_ShouldReturnTrue_WhenBothEmailAndUserNameMatch_OnTheSameRecord()
        {
            // Arrange
            const string userName = "myUserName";
            const string email = "my@email.address";

            IEnumerable<User> users = new[]
            {
                new User {Email = "Something@else.com", UserName = "Somethingelse"},
                new User {Email = "my@EMAIL.address", UserName = "myUserNAME"},
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await repository.UserNameOrEmailExists(userName, email);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UserNameOrEmailExists_ShouldReturnTrue_WhenBothEmailAndUserNameMatch_OnDifferentRecords()
        {
            // Arrange
            const string userName = "myUserName";
            const string email = "my@email.address";

            IEnumerable<User> users = new[]
            {
                new User {Email = "Something@else.com", UserName = "myUserNAME"},
                new User {Email = "my@EMAIL.address", UserName = "Somethingelse"},
            };

            Mock<DbSet<User>> userDbSetMock = users
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Users)
                .Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(contextMock.Object);

            // Act
            bool exists = await repository.UserNameOrEmailExists(userName, email);

            // Assert
            Assert.True(exists);
        }
    }
}
