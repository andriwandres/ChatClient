using System.Reflection;
using Core.Domain.Database;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Database
{
    public class ChatContext : DbContext, IChatContext
    {
        public DbSet<User> Users { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
