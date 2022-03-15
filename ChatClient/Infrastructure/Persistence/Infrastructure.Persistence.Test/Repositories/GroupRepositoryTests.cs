using Core.Application.Database;
using Core.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Test.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Persistence.Test.Repositories;

public class GroupRepositoryTests
{
    private readonly IChatContext _context;

    public GroupRepositoryTests()
    {
        _context = TestContextFactory.Create();
    }

    [Fact]
    public async Task Add_ShouldAddGroupToTheDbContext()
    {
        // Arrange
        Group group = new();

        GroupRepository repository = new(_context);

        // Act
        await repository.Add(group);

        // Assert
        Assert.NotEqual(0, group.GroupId);
        Group addedGroup = await _context.Groups.FindAsync(group.GroupId);

        Assert.NotNull(addedGroup);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenIdDoesNotMatch()
    {
        // Arrange
        const int groupId = 38749;

        IEnumerable<Group> databaseGroups = new[]
        {
            new Group { GroupId = 1, Name = "Group #1", Description = "Group Description #1" },
            new Group { GroupId = 2, Name = "Group #2", Description = "Group Description #2" },
            new Group { GroupId = 3, Name = "Group #3", Description = "Group Description #3" },
        };

        await _context.Groups.AddRangeAsync(databaseGroups);
        await _context.SaveChangesAsync();

        GroupRepository repository = new(_context);

        // Act
        Group group = await repository.GetByIdAsync(groupId);

        // Assert
        Assert.Null(group);
    }

    [Fact]
    public async Task GetById_ShouldReturnSingleGroup_WhenIdMatches()
    {
        // Arrange
        const int groupId = 1;

        IEnumerable<Group> databaseGroups = new[]
        {
            new Group { GroupId = 1, Name = "Group #1", Description = "Group Description #1" },
            new Group { GroupId = 2, Name = "Group #2", Description = "Group Description #2" },
            new Group { GroupId = 3, Name = "Group #3", Description = "Group Description #3" },
        };

        await _context.Groups.AddRangeAsync(databaseGroups);
        await _context.SaveChangesAsync();

        GroupRepository repository = new(_context);

        // Act
        Group group = await repository.GetByIdAsync(groupId);

        // Assert
        Assert.NotNull(group);
        Assert.Equal(groupId, group.GroupId);
    }

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenGroupExists()
    {
        // Arrange
        const int groupId = 1;

        IEnumerable<Group> databaseGroups = new[]
        {
            new Group { GroupId = 1, Name = "Group #1", Description = "Group Description #1" },
            new Group { GroupId = 2, Name = "Group #2", Description = "Group Description #2" },
            new Group { GroupId = 3, Name = "Group #3", Description = "Group Description #3" },
        };

        await _context.Groups.AddRangeAsync(databaseGroups);
        await _context.SaveChangesAsync();

        GroupRepository repository = new(_context);

        // Act
        bool exists = await repository.Exists(groupId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenGroupDoesNotExist()
    {
        // Arrange
        const int groupId = 41;

        IEnumerable<Group> databaseGroups = new[]
        {
            new Group { GroupId = 1, Name = "Group #1", Description = "Group Description #1" },
            new Group { GroupId = 2, Name = "Group #2", Description = "Group Description #2" },
            new Group { GroupId = 3, Name = "Group #3", Description = "Group Description #3" },
        };

        await _context.Groups.AddRangeAsync(databaseGroups);
        await _context.SaveChangesAsync();

        GroupRepository repository = new(_context);

        // Act
        bool exists = await repository.Exists(groupId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenGroupWasDeleted()
    {
        // Arrange
        const int groupId = 1;

        IEnumerable<Group> databaseGroups = new[]
        {
            new Group { GroupId = 1, Name = "Group #1", Description = "Group Description #1", IsDeleted = true },
            new Group { GroupId = 2, Name = "Group #2", Description = "Group Description #2" },
            new Group { GroupId = 3, Name = "Group #3", Description = "Group Description #3" },
        };

        await _context.Groups.AddRangeAsync(databaseGroups);
        await _context.SaveChangesAsync();

        GroupRepository repository = new(_context);

        // Act
        bool exists = await repository.Exists(groupId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task Update_ShouldUpdateGroupInContext()
    {
        // Arrange
        Group group = new() {GroupId = 1, Name = "Test Group", Description = "Updated" };
        Group databaseGroup = new() { GroupId = 1, Name = "Test Group", Description = "Original" };

        await _context.Groups.AddAsync(databaseGroup);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        GroupRepository repository = new(_context);

        // Act
        repository.Update(group);

        // Assert
        Group updatedGroup = await _context.Groups.FindAsync(group.GroupId);            

        Assert.NotNull(updatedGroup);
        Assert.Equal(group.Description, updatedGroup.Description);
    }
}