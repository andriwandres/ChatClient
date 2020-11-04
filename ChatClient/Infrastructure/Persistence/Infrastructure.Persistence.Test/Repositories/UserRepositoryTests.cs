using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
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
    }
}
