using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ChatClient.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(Users);
        }

        private readonly IEnumerable<User> Users = new HashSet<User>
        {
            new User
            {
                UserId = 1,
                UserCode = "A1C4T1",
                DisplayName = "AndriWandres",
                Email = "andri.wandres@swisslife.ch",
                PasswordHash = Convert.FromBase64String("wlD7izxOXkee+pDfrKJD037DtDTz2Yyi0E5kLRwC9W8="),
                PasswordSalt = Convert.FromBase64String("oC8ig9fbCMG63d7LCYL8qA=="),
                CreatedAt = DateTime.UtcNow,
            },
            new User
            {
                UserId = 2,
                UserCode = "T9D5W9",
                DisplayName = "User 1",
                Email = "user1@test.ch",
                PasswordHash = Convert.FromBase64String("wlD7izxOXkee+pDfrKJD037DtDTz2Yyi0E5kLRwC9W8="),
                PasswordSalt = Convert.FromBase64String("oC8ig9fbCMG63d7LCYL8qA=="),
                CreatedAt = DateTime.UtcNow,
            },
            new User
            {
                UserId = 3,
                UserCode = "S9B2I6",
                DisplayName = "User 2",
                Email = "user2@test.ch",
                PasswordHash = Convert.FromBase64String("wlD7izxOXkee+pDfrKJD037DtDTz2Yyi0E5kLRwC9W8="),
                PasswordSalt = Convert.FromBase64String("oC8ig9fbCMG63d7LCYL8qA=="),
                CreatedAt = DateTime.UtcNow,
            },
            new User
            {
                UserId = 4,
                UserCode = "E2T8N7",
                DisplayName = "User 3",
                Email = "user3@test.ch",
                PasswordHash = Convert.FromBase64String("wlD7izxOXkee+pDfrKJD037DtDTz2Yyi0E5kLRwC9W8="),
                PasswordSalt = Convert.FromBase64String("oC8ig9fbCMG63d7LCYL8qA=="),
                CreatedAt = DateTime.UtcNow,
            },
        };
    }
}
