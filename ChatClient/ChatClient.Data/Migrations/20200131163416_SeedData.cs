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
                    { 1, new DateTime(2020, 1, 31, 16, 34, 15, 326, DateTimeKind.Utc).AddTicks(849), "Description for Group 1", "Group 1" },
                    { 2, new DateTime(2020, 1, 31, 16, 34, 15, 326, DateTimeKind.Utc).AddTicks(1844), "Description for Group 2", "Group 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "DisplayName", "Email", "PasswordHash", "PasswordSalt", "UserCode" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 31, 16, 34, 15, 323, DateTimeKind.Utc).AddTicks(9125), "AndriWandres", "andri.wandres@swisslife.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "A1C4T1" },
                    { 2, new DateTime(2020, 1, 31, 16, 34, 15, 324, DateTimeKind.Utc).AddTicks(178), "User 1", "user1@test.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "T9D5W9" },
                    { 3, new DateTime(2020, 1, 31, 16, 34, 15, 324, DateTimeKind.Utc).AddTicks(200), "User 2", "user2@test.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "S9B2I6" },
                    { 4, new DateTime(2020, 1, 31, 16, 34, 15, 324, DateTimeKind.Utc).AddTicks(207), "User 3", "user3@test.ch", new byte[] { 194, 80, 251, 139, 60, 78, 94, 71, 158, 250, 144, 223, 172, 162, 67, 211, 126, 195, 180, 52, 243, 217, 140, 162, 208, 78, 100, 45, 28, 2, 245, 111 }, new byte[] { 160, 47, 34, 131, 215, 219, 8, 193, 186, 221, 222, 203, 9, 130, 252, 168 }, "E2T8N7" }
                });

            migrationBuilder.InsertData(
                table: "GroupMemberships",
                columns: new[] { "GroupMembershipId", "CreatedAt", "GroupId", "IsAdmin", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(2239), 1, true, 1 },
                    { 4, new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3196), 2, false, 1 },
                    { 2, new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3176), 1, false, 2 },
                    { 3, new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3193), 1, false, 3 },
                    { 5, new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3206), 2, true, 3 },
                    { 6, new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3208), 2, false, 4 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "AuthorId", "CreatedAt", "IsEdited", "IsForwarded", "ParentId", "TextContent" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(1842), false, false, null, "Hello User 1" },
                    { 3, 1, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4010), false, false, null, "Hello Group 1" },
                    { 7, 1, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4029), false, false, null, "Hi!" },
                    { 2, 2, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(3955), false, false, null, "Hello AndriWandres" },
                    { 4, 2, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4014), false, false, null, "Hello together!" },
                    { 6, 2, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4027), false, false, null, "Hello together!" },
                    { 5, 3, new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4024), false, false, null, "Greetings everyone!" }
                });

            migrationBuilder.InsertData(
                table: "MessageRecipients",
                columns: new[] { "MessageRecipientId", "IsRead", "MessageId", "ReadAt", "RecipientGroupId", "RecipientUserId" },
                values: new object[,]
                {
                    { 1, true, 1, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(7397), null, 2 },
                    { 3, true, 3, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8673), 2, null },
                    { 2, false, 2, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8637), null, 1 },
                    { 5, true, 4, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8687), 1, null },
                    { 9, true, 6, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8700), 4, null },
                    { 4, true, 3, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8677), 3, null },
                    { 6, true, 4, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8690), 3, null },
                    { 11, true, 7, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8704), 5, null },
                    { 7, true, 5, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8692), 1, null },
                    { 8, true, 5, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8694), 2, null },
                    { 10, true, 6, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8702), 6, null },
                    { 12, true, 7, new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8706), 6, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 12);

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
