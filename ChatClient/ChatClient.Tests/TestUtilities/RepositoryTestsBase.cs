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

        protected void SeedTestDatabase(DataContainer dataContainer)
        {
            DbContextOptions<ChatContext> options = new DbContextOptionsBuilder<ChatContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new ChatContext(options);

            // Clean and re-create database
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

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
