using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatClient.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 29, 17, 10, 22, 511, DateTimeKind.Utc).AddTicks(4982), "Description for Group 1", "Group 1" },
                    { 2, new DateTime(2020, 1, 29, 17, 10, 22, 511, DateTimeKind.Utc).AddTicks(5799), "Description for Group 2", "Group 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "DisplayName", "Email", "PasswordHash", "PasswordSalt", "UserCode" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 29, 17, 10, 22, 509, DateTimeKind.Utc).AddTicks(8798), "AndriWandres", "andri.wandres@swisslife.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "A1C4T1" },
                    { 2, new DateTime(2020, 1, 29, 17, 10, 22, 509, DateTimeKind.Utc).AddTicks(9570), "User 1", "user1@test.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "T9D5W9" },
                    { 3, new DateTime(2020, 1, 29, 17, 10, 22, 509, DateTimeKind.Utc).AddTicks(9587), "User 2", "user2@test.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "S9B2I6" },
                    { 4, new DateTime(2020, 1, 29, 17, 10, 22, 509, DateTimeKind.Utc).AddTicks(9592), "User 3", "user3@test.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "E2T8N7" }
                });

            migrationBuilder.InsertData(
                table: "GroupMemberships",
                columns: new[] { "GroupMembershipId", "CreatedAt", "GroupId", "IsAdmin", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 29, 17, 10, 22, 512, DateTimeKind.Utc).AddTicks(1661), 1, true, 1 },
                    { 4, new DateTime(2020, 1, 29, 17, 10, 22, 512, DateTimeKind.Utc).AddTicks(2359), 2, false, 1 },
                    { 2, new DateTime(2020, 1, 29, 17, 10, 22, 512, DateTimeKind.Utc).AddTicks(2344), 1, false, 2 },
                    { 3, new DateTime(2020, 1, 29, 17, 10, 22, 512, DateTimeKind.Utc).AddTicks(2357), 1, false, 3 },
                    { 5, new DateTime(2020, 1, 29, 17, 10, 22, 512, DateTimeKind.Utc).AddTicks(2367), 2, true, 3 },
                    { 6, new DateTime(2020, 1, 29, 17, 10, 22, 512, DateTimeKind.Utc).AddTicks(2369), 2, false, 4 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "AuthorId", "CreatedAt", "IsEdited", "IsForwarded", "ParentId", "TextContent" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(91), false, false, null, "Hello User 1" },
                    { 3, 1, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(1645), false, false, null, "Hello Group 1" },
                    { 7, 1, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(1658), false, false, null, "Hi!" },
                    { 2, 2, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(1619), false, false, null, "Hello AndriWandres" },
                    { 4, 2, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(1647), false, false, null, "Hello together!" },
                    { 6, 2, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(1656), false, false, null, "Hello together!" },
                    { 5, 3, new DateTime(2020, 1, 29, 17, 10, 22, 513, DateTimeKind.Utc).AddTicks(1654), false, false, null, "Greetings everyone!" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);
        }
    }
}
