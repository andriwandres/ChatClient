using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories;

public class UserRepositoryTests
{
    private readonly IChatContext _context;

    public UserRepositoryTests()
    {
        _context = TestContextFactory.Create();
    }

    #region GetByUserNameOrEmail()

    [Fact]
    public async Task GetByUserNameOrEmail_ShouldReturnUser_WhenEmailMatches()
    {
        // Arrange
        const string email = "user1@test.com";

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(_context);

        // Act
        User user = await userRepository.GetByUserNameOrEmail(email);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(email, user.Email);
    }

    [Fact]
    public async Task GetByUserNameOrEmail_ShouldReturnUser_WhenEmailMatches_WithDifferentCase()
    {
        // Arrange
        const string email = "USER1@TEST.COM";

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(_context);

        // Act
        User user = await userRepository.GetByUserNameOrEmail(email);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(email.ToLower(), user.Email.ToLower());
    }

    [Fact]
    public async Task GetByUserNameOrEmail_ShouldReturnUser_WhenUserNameMatches()
    {
        // Arrange
        const string userName = "User1";

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(_context);

        // Act
        User user = await userRepository.GetByUserNameOrEmail(userName);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userName, user.UserName);
    }

    [Fact]
    public async Task GetByUserNameOrEmail_ShouldReturnUser_WhenUserNameMatches_WithDifferentCase()
    {
        // Arrange
        const string userName = "USER1";

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(_context);

        // Act
        User user = await userRepository.GetByUserNameOrEmail(userName);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userName.ToLower(), user.UserName.ToLower());
    }

    [Fact]
    public async Task GetByUserNameOrEmail_ShouldReturnNull_WhenUserNameOrEmailAreInvalid()
    {
        // Arrange
        const string input = "xxx_invalid_xxx";

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(_context);

        // Act
        User user = await userRepository.GetByUserNameOrEmail(input);

        // Assert
        Assert.Null(user);
    }

    #endregion

    #region Add()

    [Fact]
    public async Task Add_ShouldAddUser()
    {
        // Arrange
        User user = new()
        {
            UserName = "myUsername",
            Email = "my@email.address",
            PasswordHash = Array.Empty<byte>(),
            PasswordSalt = Array.Empty<byte>()
        };

        IUserRepository userRepository = new UserRepository(_context);

        // Act
        await userRepository.Add(user);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        Assert.NotEqual(0, user.UserId);

        User addedUser = await _context.Users.FindAsync(user.UserId);

        Assert.NotNull(addedUser);
    }

    #endregion

    #region UserNameOrEmailExists()

    [Fact]
    public async Task UserNameOrEmailExists_ShouldReturnFalse_WhenNeitherUserNameNorEmailMatch()
    {
        // Arrange
        const string userName = "myUserName";
        const string email = "my@email.address";

        IEnumerable<User> users = new[]
        {
            new User {Email = "Something@else.com", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "Something@else.again", UserName = "Somethingelseagain", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

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
            new User {Email = "Something@else.com", UserName = "MyUSERNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "Something@else.again", UserName = "Somethingelseagain", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

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
            new User {Email = "Something@else.com", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelseagain", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

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
            new User {Email = "Something@else.com", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

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
            new User {Email = "Something@else.com", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.UserNameOrEmailExists(userName, email);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task UserNameOrEmailExists_ShouldReturnTrue_WhenEmailIsNull_AndUserNameMatches()
    {
        // Arrange
        const string email = null;
        const string userName = "myUserName";

        IEnumerable<User> users = new[]
        {
            new User {Email = "Something@else.com", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.UserNameOrEmailExists(userName, email);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task UserNameOrEmailExists_ShouldReturnTrue_WhenUserNameIsNull_AndEmailMatches()
    {
        // Arrange
        const string email = "my@email.address";
        const string userName = null;

        IEnumerable<User> users = new[]
        {
            new User {Email = "Something@else.com", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.UserNameOrEmailExists(userName, email);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task UserNameOrEmailExists_ShouldReturnFalse_WhenUserNameIsNull_AndEmailDoesNotMatch()
    {
        // Arrange
        const string email = "someones@other-email.address";
        const string userName = null;

        IEnumerable<User> users = new[]
        {
            new User {Email = "Something@else.com", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.UserNameOrEmailExists(userName, email);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task UserNameOrEmailExists_ShouldReturnFalse_WhenEmailIsNull_AndUserNameDoesNotMatch()
    {
        // Arrange
        const string email = null;
        const string userName = "thisuserdoesnotexist";

        IEnumerable<User> users = new[]
        {
            new User {Email = "Something@else.com", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.UserNameOrEmailExists(userName, email);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task UserNameOrEmailExists_ShouldReturnFalse_WhenBothEmailAndUserNameAreNull()
    {
        // Arrange
        const string email = null;
        const string userName = null;

        IEnumerable<User> users = new[]
        {
            new User {Email = "Something@else.com", UserName = "myUserNAME", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
            new User {Email = "my@EMAIL.address", UserName = "Somethingelse", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>()},
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.UserNameOrEmailExists(userName, email);

        // Assert
        Assert.False(exists);
    }

    #endregion

    #region Exists()

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenIdMatches()
    {
        // Arrange
        const int userId = 1;

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 3, Email = "user3@test.com", UserName = "User3", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.Exists(userId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenIdDoesNotMatch()
    {
        // Arrange
        const int userId = 48721;

        IEnumerable<User> users = new[]
        {
            new User { UserId = 1, Email = "user1@test.com", UserName = "User1", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 2, Email = "user2@test.com", UserName = "User2", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { UserId = 3, Email = "user3@test.com", UserName = "User3", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        IUserRepository repository = new UserRepository(_context);

        // Act
        bool exists = await repository.Exists(userId);

        // Assert
        Assert.False(exists);
    }

    #endregion
}