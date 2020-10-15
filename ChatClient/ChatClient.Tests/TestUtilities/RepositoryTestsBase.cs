using ChatClient.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Tests.TestUtilities
{
    public abstract class RepositoryTestsBase : IDisposable
    {
        protected ChatContext Context { get; private set; }

        public RepositoryTestsBase()
        {
            // Create in-memory database for testing
            DbContextOptions<ChatContext> options = new DbContextOptionsBuilder<ChatContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new ChatContext(options);

            Context.Database.EnsureCreated();
        }

        protected void SeedDatabase(DataContainer dataContainer)
        {
            // Add entities from mapping container
            Context.Users.AddRange(dataContainer.Users);
            Context.Groups.AddRange(dataContainer.Groups);
            Context.Messages.AddRange(dataContainer.Messages);
            Context.UserRelationships.AddRange(dataContainer.UserRelationships);
            Context.MessageRecipients.AddRange(dataContainer.MessageRecipients);
            Context.GroupMemberships.AddRange(dataContainer.GroupMemberships);

            Context.SaveChanges();
        }

        public void Dispose()
        {
            // Delete the test database
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
