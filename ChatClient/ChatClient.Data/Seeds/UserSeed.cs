using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(UserData);
        }

        private readonly IEnumerable<User> UserData = new List<User>
        {
            new User
            {
                UserId = 1,
                UserCode = "A1C4T1",
                DisplayName = "AndriWandres",
                Email = "andri.wandres@swisslife.ch",
                PasswordHash = Encoding.UTF8.GetBytes("0x0D5D6AA8009B864616F772EAE4AC9C44041F0C4503B6DBAAD0D7EA4358112737"),
                PasswordSalt = Encoding.UTF8.GetBytes("0x0DF29FD155387E53D2F3A5E484BF9305"),
                CreatedAt = DateTime.UtcNow,
            },
            new User
            {
                UserId = 2,
                UserCode = "T9D5W9",
                DisplayName = "User 1",
                Email = "user1@test.ch",
                PasswordHash = Encoding.UTF8.GetBytes("0x7E602F28F1A219D5E61D1D638DDED960A5CC98DE7F40233661CB829DB11CEC40"),
                PasswordSalt = Encoding.UTF8.GetBytes("0x1CB06B656B6482386757383EA373A171"),
                CreatedAt = DateTime.UtcNow,
            },
            new User
            {
                UserId = 3,
                UserCode = "S9B2I6",
                DisplayName = "User 2",
                Email = "user2@test.ch",
                PasswordHash = Encoding.UTF8.GetBytes("0x79CCD31B59CC5899054CA14790ED5331136448CC55270743635B10EC6002B1FB"),
                PasswordSalt = Encoding.UTF8.GetBytes("0x12BA7631C487E5C83C877DF5546C47BE"),
                CreatedAt = DateTime.UtcNow,
            },
            new User
            {
                UserId = 4,
                UserCode = "E2T8N7",
                DisplayName = "User 3",
                Email = "user3@test.ch",
                PasswordHash = Encoding.UTF8.GetBytes("0xD578027B382B3EB387758DC6C7FAF15D4DED0E171335612C58A4164275079777"),
                PasswordSalt = Encoding.UTF8.GetBytes("0xDE9003F6C5280351D797E574FC39C7DB"),
                CreatedAt = DateTime.UtcNow,
            },
        };
    }
}
