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
    public class GroupMembershipRepositoryTests
    {
        [Fact]
        public async Task Add_ShouldAddGroupMembershipsToTheDbContext()
        {
            // Arrange
            GroupMembership membership = new GroupMembership();

            Mock<DbSet<GroupMembership>> membershipDbSetMock = Enumerable
                .Empty<GroupMembership>()
                .AsQueryable()
                .BuildMockDbSet();

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock.Object);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            await repository.Add(membership);

            // Assert
            contextMock.Verify(m => m.GroupMemberships.AddAsync(membership, It.IsAny<CancellationToken>()));
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

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            IEnumerable<GroupMembership> actualMemberships = await repository
                .GetByGroup(groupId)
                .ToListAsync();

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

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool exists = await repository.CombinationExists(groupId, userId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task GetById_ShouldReturnEmptyQueryable_WhenIdDoesNotMatch()
        {
            // Arrange
            const int membershipId = 43289;

            IEnumerable<GroupMembership> databaseMemberships = new[]
            {
                new GroupMembership { GroupMembershipId = 1, GroupId = 1, UserId = 1 },
                new GroupMembership { GroupMembershipId = 2, GroupId = 1, UserId = 2},
                new GroupMembership { GroupMembershipId = 3, GroupId = 2, UserId = 1 },
                new GroupMembership { GroupMembershipId = 4, GroupId = 3, UserId = 1},
            };

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            IEnumerable<GroupMembership> memberships = await repository
                .GetById(membershipId)
                .ToListAsync();

            // Assert
            Assert.NotNull(memberships);
            Assert.Empty(memberships);
        }

        [Fact]
        public async Task GetById_ShouldReturnMembership_WhenIdMatches()
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

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            GroupMembership membership = await repository
                .GetById(membershipId)
                .SingleOrDefaultAsync();

            // Assert
            Assert.NotNull(membership);
            Assert.Equal(membershipId, membership.GroupMembershipId);
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

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            DbSet<GroupMembership> membershipDbSetMock = databaseMemberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(membershipDbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool exists = await repository.Exists(membershipId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public void Update_ShouldUpdateMembership()
        {
            // Arrange
            DbSet<GroupMembership> dbSetMock = Enumerable
                .Empty<GroupMembership>()
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            repository.Update(new GroupMembership());

            // Assert
            contextMock.Verify(m => m.GroupMemberships.Update(It.IsAny<GroupMembership>()));
        }

        [Fact]
        public async Task GetByCombination_ShouldReturnEmptyQueryable_WhenCombinationDoesNotMatch()
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

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            GroupMembership actualMembership = await repository
                .GetByCombination(groupId, userId)
                .SingleOrDefaultAsync();

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

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            GroupMembership actualMembership = await repository
                .GetByCombination(groupId, userId)
                .SingleOrDefaultAsync();

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

            IEnumerable<GroupMembership> memberships = new[]
            {
                new GroupMembership 
                {
                    GroupMembershipId = 1, 
                    GroupId = 1, 
                    UserId = 2,
                    Group = new Group
                    {
                        Memberships = new HashSet<GroupMembership>
                        {
                            new GroupMembership { GroupMembershipId = 2, UserId = 2, IsAdmin = true }
                        }
                    }
                },
            };

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            IEnumerable<GroupMembership> memberships = new[]
            {
                new GroupMembership
                {
                    GroupMembershipId = 1,
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        Memberships = new HashSet<GroupMembership>
                        {
                            new GroupMembership { GroupMembershipId = 2, UserId = 1, IsAdmin = false }
                        }
                    }
                },
            };

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            IEnumerable<GroupMembership> memberships = new[]
            {
                new GroupMembership
                {
                    GroupMembershipId = 1,
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        Memberships = new HashSet<GroupMembership>
                        {
                            new GroupMembership { GroupMembershipId = 2, UserId = 1, IsAdmin = true }
                        }
                    }
                },
            };

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            IEnumerable<GroupMembership> memberships = new[]
            {
                new GroupMembership
                {
                    GroupMembershipId = 1,
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        Memberships = new HashSet<GroupMembership>
                        {
                            new GroupMembership { GroupMembershipId = 2, UserId = 2, IsAdmin = true }
                        }
                    }
                },
            };

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            IEnumerable<GroupMembership> memberships = new[]
            {
                new GroupMembership
                {
                    GroupMembershipId = 1,
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        Memberships = new HashSet<GroupMembership>
                        {
                            new GroupMembership { GroupMembershipId = 2, UserId = 1, IsAdmin = false }
                        }
                    }
                },
            };

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

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

            IEnumerable<GroupMembership> memberships = new[]
            {
                new GroupMembership
                {
                    GroupMembershipId = 1,
                    GroupId = 1,
                    UserId = 2,
                    Group = new Group
                    {
                        Memberships = new HashSet<GroupMembership>
                        {
                            new GroupMembership { GroupMembershipId = 2, UserId = 1, IsAdmin = true }
                        }
                    }
                },
            };

            DbSet<GroupMembership> dbSetMock = memberships
                .AsQueryable()
                .BuildMockDbSet()
                .Object;

            Mock<IChatContext> contextMock = new Mock<IChatContext>();
            contextMock
                .Setup(m => m.GroupMemberships)
                .Returns(dbSetMock);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            bool canDelete = await repository.CanDeleteMembership(userId, membershipIdToDelete);

            // Assert
            Assert.True(canDelete);
        }

        [Fact]
        public void Delete_ShouldDeleteMembershipFromContext()
        {
            // Arrange
            GroupMembership membership = new GroupMembership {GroupMembershipId = 1};

            Mock<IChatContext> contextMock = new Mock<IChatContext>();

            GroupMembership passedMembership = null;

            contextMock
                .Setup(m => m.GroupMemberships.Remove(It.IsAny<GroupMembership>()))
                .Callback<GroupMembership>(gm => passedMembership = gm);

            GroupMembershipRepository repository = new GroupMembershipRepository(contextMock.Object);

            // Act
            repository.Delete(membership);

            // Assert
            contextMock.Verify(m => m.GroupMemberships.Remove(It.IsAny<GroupMembership>()));

            Assert.NotNull(passedMembership);
            Assert.Equal(membership.GroupMembershipId, passedMembership.GroupMembershipId);
        }
    }
}
