using Core.Application.Database;
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
    public class GroupRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddGroupToTheDbContext()
        {
            // Arrange
            Group group = new Group();

            Mock<DbSet<Group>> groupDbSetMock = Enumerable
                .Empty<Group>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(groupDbSetMock.Object);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            await repository.Add(group);

            // Assert
            contextMock.Verify(m => m.Groups.AddAsync(group, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotMatch()
        {
            // Arrange
            const int groupId = 38749;

            IEnumerable<Group> databaseGroups = new[]
            {
                new Group {GroupId = 1},
                new Group {GroupId = 2},
                new Group {GroupId = 3},
            };

            Mock<DbSet<Group>> groupDbSetMock = databaseGroups
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(groupDbSetMock.Object);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            IEnumerable<Group> group = await repository
                .GetById(groupId)
                .ToListAsync();

            // Assert
            Assert.NotNull(group);
            Assert.Empty(group);
        }

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenGroupWasDeleted()
        {
            // Arrange
            const int groupId = 1;

            IEnumerable<Group> databaseGroups = new[]
            {
                new Group {GroupId = 1, IsDeleted = true },
                new Group {GroupId = 2},
                new Group {GroupId = 3},
            };

            Mock<DbSet<Group>> groupDbSetMock = databaseGroups
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(groupDbSetMock.Object);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            IEnumerable<Group> group = await repository
                .GetById(groupId)
                .ToListAsync();

            // Assert
            Assert.NotNull(group);
            Assert.Empty(group);
        }

        [Fact]
        public async Task GetById_ShouldReturnSingleGroup_WhenIdMatches()
        {
            // Arrange
            const int groupId = 1;

            IEnumerable<Group> databaseGroups = new[]
            {
                new Group {GroupId = 1},
                new Group {GroupId = 2},
                new Group {GroupId = 3},
            };

            Mock<DbSet<Group>> groupDbSetMock = databaseGroups
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(groupDbSetMock.Object);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            Group group = await repository
                .GetById(groupId)
                .SingleOrDefaultAsync();

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
                new Group { GroupId = 1 },
                new Group { GroupId = 2 },
                new Group { GroupId = 3 },
            };

            DbSet<Group> mockDbSet = databaseGroups
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(mockDbSet);

            GroupRepository repository = new GroupRepository(contextMock.Object);

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
                new Group { GroupId = 1 },
                new Group { GroupId = 2 },
                new Group { GroupId = 3 },
            };

            DbSet<Group> mockDbSet = databaseGroups
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(mockDbSet);

            GroupRepository repository = new GroupRepository(contextMock.Object);

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
                new Group { GroupId = 1, IsDeleted = true },
                new Group { GroupId = 2 },
                new Group { GroupId = 3 },
            };

            DbSet<Group> mockDbSet = databaseGroups
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(mockDbSet);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            bool exists = await repository.Exists(groupId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public void Update_ShouldUpdateGroupInContext()
        {
            // Arrange
            Group group = new Group {GroupId = 1};

            IEnumerable<Group> databaseGroups = new []
            {
                new Group { GroupId = 1 }
            };

            DbSet<Group> mockDbSet = databaseGroups
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.Groups)
                .Returns(mockDbSet);

            GroupRepository repository = new GroupRepository(contextMock.Object);

            // Act
            repository.Update(new Group());

            // Assert
            contextMock.Verify(m => m.Groups.Update(It.IsAny<Group>()), Times.Once);
        }
    }
}
