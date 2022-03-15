using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories;

public class GroupMembershipRepositoryTests
{
    private readonly IChatContext _context;

    public GroupMembershipRepositoryTests()
    {
        _context = TestContextFactory.Create();
    }

    [Fact]
    public async Task Add_ShouldAddGroupMembershipsToTheDbContext()
    {
        // Arrange
        GroupMembership membership = new();

        IGroupMembershipRepository repository = new GroupMembershipRepository(_context);

        // Act
        await repository.Add(membership);

        // Assert
        Assert.NotEqual(0, membership.GroupMembershipId);
        GroupMembership addedMembership = await _context.GroupMemberships.FindAsync(membership.GroupMembershipId);

        Assert.NotNull(addedMembership);
    }

    [Fact]
    public async Task GetByGroup_ShouldGetMemberships()
    {
        // Arrange
        const int groupId = 1;

        IEnumerable<GroupMembership> databaseMemberships = new []
        {
            new GroupMembership { GroupMembershipId = 1, GroupId = 1 },
            new GroupMembership { GroupMembershipId = 2, GroupId = 1 },
            new GroupMembership { GroupMembershipId = 3, GroupId = 2 },
            new GroupMembership { GroupMembershipId = 4, GroupId = 3 },
        };

        await _context.GroupMemberships.AddRangeAsync(databaseMemberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        IEnumerable<GroupMembership> actualMemberships = await repository.GetByGroup(groupId);

        // Assert
        Assert.NotNull(actualMemberships);
        Assert.Equal(2, actualMemberships.Count());
        Assert.All(actualMemberships, m => Assert.Equal(groupId, m.GroupId));
    }

    [Fact]
    public async Task CombinationExists_ShouldReturnTrue_WhenCombinationExists()
    {
        // Arrange
        const int userId = 1;
        const int groupId = 2;

        IEnumerable<GroupMembership> databaseMemberships = new[]
        {
            new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
            new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
            new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
            new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
        };

        await _context.GroupMemberships.AddRangeAsync(databaseMemberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool exists = await repository.CombinationExists(groupId, userId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task CombinationExists_ShouldReturnFalse_WhenCombinationDoesNotExist()
    {
        // Arrange
        const int userId = 2131;
        const int groupId = 412;

        IEnumerable<GroupMembership> databaseMemberships = new[]
        {
            new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
            new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
            new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
            new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
        };

        await _context.GroupMemberships.AddRangeAsync(databaseMemberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool exists = await repository.CombinationExists(groupId, userId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenIdMatches()
    {
        // Arrange
        const int membershipId = 2;

        IEnumerable<GroupMembership> databaseMemberships = new[]
        {
            new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
            new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
            new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
            new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
        };

        await _context.GroupMemberships.AddRangeAsync(databaseMemberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool exists = await repository.Exists(membershipId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenIdDoesNotMatch()
    {
        // Arrange
        const int membershipId = 5452;

        IEnumerable<GroupMembership> databaseMemberships = new[]
        {
            new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
            new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
            new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
            new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
        };

        await _context.GroupMemberships.AddRangeAsync(databaseMemberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool exists = await repository.Exists(membershipId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task Update_ShouldUpdateMembership()
    {
        // Arrange
        GroupMembership databaseMembership = new() { GroupMembershipId = 1, UserId = 1 };
        GroupMembership membershipToUpdate = new() { GroupMembershipId = 2, UserId = 2 };

        await _context.GroupMemberships.AddAsync(databaseMembership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        repository.Update(membershipToUpdate);

        // Assert
        GroupMembership updatedMembership = await _context.GroupMemberships.FindAsync(membershipToUpdate.GroupMembershipId);

        Assert.NotNull(updatedMembership);
        Assert.Equal(membershipToUpdate.UserId, updatedMembership.UserId);
    }

    [Fact]
    public async Task GetByCombination_ShouldReturnNull_WhenCombinationDoesNotMatch()
    {
        // Arrange
        const int userId = 4311;
        const int groupId = 411;

        IEnumerable<GroupMembership> memberships = new[]
        {
            new GroupMembership {GroupMembershipId = 1, GroupId = 1, UserId = 2},
            new GroupMembership {GroupMembershipId = 2, GroupId = 1, UserId = 1},
            new GroupMembership {GroupMembershipId = 3, GroupId = 2, UserId = 1},
            new GroupMembership {GroupMembershipId = 4, GroupId = 2, UserId = 2},
        };

        await _context.GroupMemberships.AddRangeAsync(memberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        GroupMembership actualMembership = await repository.GetByCombination(groupId, userId);

        // Assert
        Assert.Null(actualMembership);
    }

    [Fact]
    public async Task GetByCombination_ShouldReturnMembership_WhenCombinationMatches()
    {
        // Arrange
        const int userId = 1;
        const int groupId = 1;

        IEnumerable<GroupMembership> memberships = new[]
        {
            new GroupMembership {GroupMembershipId = 1, GroupId = 1, UserId = 2},
            new GroupMembership {GroupMembershipId = 2, GroupId = 1, UserId = 1},
            new GroupMembership {GroupMembershipId = 3, GroupId = 2, UserId = 1},
            new GroupMembership {GroupMembershipId = 4, GroupId = 2, UserId = 2},
        };

        await _context.GroupMemberships.AddRangeAsync(memberships);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        GroupMembership actualMembership = await repository.GetByCombination(groupId, userId);

        // Assert
        Assert.NotNull(actualMembership);
        Assert.Equal(2, actualMembership.GroupMembershipId);
    }

    [Fact]
    public async Task CanUpdateMembership_ShouldReturnFalse_WhenTheUserIsNotPartOfTheGroup()
    {
        // Arrange
        const int userId = 1;
        const int membershipIdToUpdate = 1;

        GroupMembership membership = new()
        {
            GroupMembershipId = 1, 
            GroupId = 1, 
            UserId = 2,
            Group = new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Memberships = new HashSet<GroupMembership>
                {
                    new() { GroupMembershipId = 2, UserId = 2, IsAdmin = true }
                }
            }
        };

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool canUpdate = await repository.CanUpdateMembership(userId, membershipIdToUpdate);

        // Assert
        Assert.False(canUpdate);
    }

    [Fact]
    public async Task CanUpdateMembership_ShouldReturnFalse_WhenTheUserIsNotAnAdministrator()
    {
        // Arrange
        const int userId = 1;
        const int membershipIdToUpdate = 1;

        GroupMembership membership = new()
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 2,
            Group = new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Memberships = new HashSet<GroupMembership>
                {
                    new() { GroupMembershipId = 2, UserId = 1, IsAdmin = false }
                }
            }
        };

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool canUpdate = await repository.CanUpdateMembership(userId, membershipIdToUpdate);

        // Assert
        Assert.False(canUpdate);
    }

    [Fact]
    public async Task CanUpdateMembership_ShouldReturnTrue_WhenTheUserIsAnAdministrator()
    {
        // Arrange
        const int userId = 1;
        const int membershipIdToUpdate = 1;

        GroupMembership membership = new GroupMembership
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 2,
            Group = new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Memberships = new HashSet<GroupMembership>
                {
                    new() { GroupMembershipId = 2, UserId = 1, IsAdmin = true }
                }
            }
        };

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool canUpdate = await repository.CanUpdateMembership(userId, membershipIdToUpdate);

        // Assert
        Assert.True(canUpdate);
    }

    [Fact]
    public async Task CanDeleteMembership_ShouldReturnFalse_WhenTheUserIsNotPartOfTheGroup()
    {
        // Arrange
        const int userId = 1;
        const int membershipIdToDelete = 1;

        GroupMembership membership = new()
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 2,
            Group = new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Memberships = new HashSet<GroupMembership>
                {
                    new() { GroupMembershipId = 2, UserId = 2, IsAdmin = true }
                }
            }
        };

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool canDelete = await repository.CanDeleteMembership(userId, membershipIdToDelete);

        // Assert
        Assert.False(canDelete);
    }

    [Fact]
    public async Task CanDeleteMembership_ShouldReturnFalse_WhenTheUserIsNotAnAdministrator()
    {
        // Arrange
        const int userId = 1;
        const int membershipIdToDelete = 1;

        GroupMembership membership = new()
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 2,
            Group = new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Memberships = new HashSet<GroupMembership>
                {
                    new() { GroupMembershipId = 2, UserId = 1, IsAdmin = false }
                }
            }
        };

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool canDelete = await repository.CanDeleteMembership(userId, membershipIdToDelete);

        // Assert
        Assert.False(canDelete);
    }

    [Fact]
    public async Task CanDeleteMembership_ShouldReturnTrue_WhenTheUserIsAnAdministrator()
    {
        // Arrange
        const int userId = 1;
        const int membershipIdToDelete = 1;

        GroupMembership membership = new()
        {
            GroupMembershipId = 1,
            GroupId = 1,
            UserId = 2,
            Group = new Group
            {
                Name = "Test Group",
                Description = "Test Group Description",
                Memberships = new HashSet<GroupMembership>
                {
                    new() { GroupMembershipId = 2, UserId = 1, IsAdmin = true }
                }
            }
        };

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        // Act
        bool canDelete = await repository.CanDeleteMembership(userId, membershipIdToDelete);

        // Assert
        Assert.True(canDelete);
    }

    [Fact]
    public async Task Delete_ShouldDeleteMembershipFromContext()
    {
        // Arrange
        GroupMembership membership = new();

        await _context.GroupMemberships.AddAsync(membership);
        await _context.SaveChangesAsync();

        GroupMembershipRepository repository = new(_context);

        membership = await _context.GroupMemberships.FindAsync(membership.GroupMembershipId);

        // Act
        repository.Delete(membership);

        await _context.SaveChangesAsync();

        // Assert
        GroupMembership deletedMembership = await _context.GroupMemberships.FindAsync(membership.GroupMembershipId);

        Assert.Null(deletedMembership);
    }
}