using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories
{
    public class FriendshipRepositoryTests
    {
        private readonly IChatContext _context;

        public FriendshipRepositoryTests()
        {
            _context = TestContextFactory.Create();
        }

        [Fact]
        public async Task GetById_ShouldReturnFriendship_WhenIdMatches()
        {
            // Arrange
            const int friendshipId = 1;

            Friendship expectedFriendship = new() {FriendshipId = 1};

            await _context.Friendships.AddAsync(expectedFriendship);
            await _context.SaveChangesAsync();

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            Friendship friendship = await repository.GetByIdAsync(friendshipId);

            // Assert
            Assert.NotNull(friendship);
            Assert.Equal(friendshipId, friendship.FriendshipId);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            const int friendshipId = 21231;

            Friendship expectedFriendship = new() {FriendshipId = 1};

            await _context.Friendships.AddAsync(expectedFriendship);
            await _context.SaveChangesAsync();

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            Friendship friendship = await repository.GetByIdAsync(friendshipId);

            // Assert
            Assert.Null(friendship);
        }

        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenFriendshipExists()
        {
            // Arrange
            const int friendshipId = 1;

            Friendship expectedFriendship = new() {FriendshipId = 1};

            await _context.Friendships.AddAsync(expectedFriendship);
            await _context.SaveChangesAsync();

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            bool exists = await repository.Exists(friendshipId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Exists_ShouldReturnFalse_WhenFriendshipDoesNotExist()
        {
            // Arrange
            const int friendshipId = 9881641;

            Friendship expectedFriendship = new() {FriendshipId = 1};

            await _context.Friendships.AddAsync(expectedFriendship);
            await _context.SaveChangesAsync();

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            bool exists = await repository.Exists(friendshipId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task Add_ShouldAddFriendship()
        {
            // Arrange
            Friendship friendship = new() { RequesterId = 1, AddresseeId = 1 };

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            await repository.Add(friendship);

            // Assert
            Assert.NotEqual(0, friendship.FriendshipId);
            Friendship addedFriendship = await _context.Friendships.FindAsync(friendship.FriendshipId);

            Assert.NotNull(addedFriendship);
        }

        [Fact]
        public async Task GetByUser_ShouldGetFriendships()
        {
            // Arrange
            const int userId = 1;

            IEnumerable<Friendship> friendships = new[]
            {
                new Friendship { FriendshipId = 1, RequesterId = 1, AddresseeId = 2}, // Match #1
                new Friendship { FriendshipId = 2, RequesterId = 3, AddresseeId = 1},
                new Friendship { FriendshipId = 3, RequesterId = 3, AddresseeId = 2}, // Match #2
                new Friendship { FriendshipId = 4, RequesterId = 2, AddresseeId = 4},
            };

            await _context.Friendships.AddRangeAsync(friendships);
            await _context.SaveChangesAsync();

            FriendshipRepository repository = new(_context);

            // Act
            IEnumerable<Friendship> actualFriendships = await repository.GetByUser(userId);

            // Assert
            Assert.Equal(2, actualFriendships.Count());
            Assert.All(actualFriendships, 
                friendship => Assert.True(friendship.RequesterId == userId || friendship.AddresseeId == userId)
            );
        }

        [Fact]
        public async Task CombinationExists_ShouldReturnTrue_WhenIdsInCombinationExist()
        {
            // Arrange
            const int requesterId = 1;
            const int addresseeId = 2;

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1, RequesterId = 1, AddresseeId = 3},
                new Friendship {FriendshipId = 2, RequesterId = 2, AddresseeId = 1},
                new Friendship {FriendshipId = 3, RequesterId = 1, AddresseeId = 5},
                new Friendship {FriendshipId = 4, RequesterId = 4, AddresseeId = 1},
            };

            await _context.Friendships.AddRangeAsync(expectedFriendship);
            await _context.SaveChangesAsync();

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            bool exists = await repository.CombinationExists(requesterId, addresseeId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task CombinationExists_ShouldReturnFalse_WhenIdsInCombinationDontExist()
        {
            // Arrange
            const int requesterId = 4351;
            const int addresseeId = 12;

            IEnumerable<Friendship> expectedFriendship = new[]
            {
                new Friendship {FriendshipId = 1, RequesterId = 1, AddresseeId = 3},
                new Friendship {FriendshipId = 2, RequesterId = 2, AddresseeId = 1},
                new Friendship {FriendshipId = 3, RequesterId = 1, AddresseeId = 5},
                new Friendship {FriendshipId = 4, RequesterId = 4, AddresseeId = 1},
            };

            await _context.Friendships.AddRangeAsync(expectedFriendship);
            await _context.SaveChangesAsync();

            IFriendshipRepository repository = new FriendshipRepository(_context);

            // Act
            bool exists = await repository.CombinationExists(requesterId, addresseeId);

            // Assert
            Assert.False(exists);
        }
    }
}