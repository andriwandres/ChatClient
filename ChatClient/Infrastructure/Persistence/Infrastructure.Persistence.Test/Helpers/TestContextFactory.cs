using Core.Application.Database;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Persistence.Test.Helpers;

public class TestContextFactory
{
    public static IChatContext Create()
    {
        DbContextOptions<ChatContext> options = new DbContextOptionsBuilder<ChatContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;

        return new ChatContext(options);
    }
}