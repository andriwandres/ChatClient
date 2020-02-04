using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatClient.Data.Migrations
{
    public partial class UserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRelationships",
                columns: table => new
                {
                    UserRelationshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitiatorId = table.Column<int>(nullable: false),
                    TargetId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelationships", x => x.UserRelationshipId);
                    table.ForeignKey(
                        name: "FK_UserRelationships_Users_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserRelationships_Users_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 807, DateTimeKind.Utc).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 807, DateTimeKind.Utc).AddTicks(5021));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 807, DateTimeKind.Utc).AddTicks(5049));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 807, DateTimeKind.Utc).AddTicks(5053));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 807, DateTimeKind.Utc).AddTicks(5067));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 807, DateTimeKind.Utc).AddTicks(5071));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 804, DateTimeKind.Utc).AddTicks(899));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 804, DateTimeKind.Utc).AddTicks(2417));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 1,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 809, DateTimeKind.Utc).AddTicks(9127));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 2,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(815));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 3,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 4,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 5,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(893));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 6,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(897));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 7,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(901));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 8,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(905));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 9,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(913));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 10,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(917));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 11,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 12,
                column: "ReadAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 810, DateTimeKind.Utc).AddTicks(925));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 805, DateTimeKind.Utc).AddTicks(7407));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 806, DateTimeKind.Utc).AddTicks(480));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 806, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 806, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 806, DateTimeKind.Utc).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 806, DateTimeKind.Utc).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 806, DateTimeKind.Utc).AddTicks(579));

            migrationBuilder.InsertData(
                table: "UserRelationships",
                columns: new[] { "UserRelationshipId", "CreatedAt", "InitiatorId", "Message", "Status", "TargetId" },
                values: new object[,]
                {
                    { 3, new DateTime(2020, 2, 4, 16, 49, 48, 811, DateTimeKind.Utc).AddTicks(3472), 4, null, 0, 1 },
                    { 2, new DateTime(2020, 2, 4, 16, 49, 48, 811, DateTimeKind.Utc).AddTicks(3444), 1, null, 0, 3 },
                    { 1, new DateTime(2020, 2, 4, 16, 49, 48, 811, DateTimeKind.Utc).AddTicks(1815), 1, null, 1, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 801, DateTimeKind.Utc).AddTicks(426));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 801, DateTimeKind.Utc).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 801, DateTimeKind.Utc).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 801, DateTimeKind.Utc).AddTicks(1796));

            migrationBuilder.CreateIndex(
                name: "IX_UserRelationships_InitiatorId",
                table: "UserRelationships",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelationships_TargetId",
                table: "UserRelationships",
                column: "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRelationships");

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3176));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3193));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3196));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3206));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 328, DateTimeKind.Utc).AddTicks(3208));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 326, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 326, DateTimeKind.Utc).AddTicks(1844));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 1,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 2,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8637));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 3,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 4,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 5,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 6,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 7,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8692));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 8,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 9,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8700));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 10,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8702));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 11,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8704));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 12,
                column: "ReadAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 329, DateTimeKind.Utc).AddTicks(8706));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(1842));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(3955));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4024));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 327, DateTimeKind.Utc).AddTicks(4029));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 323, DateTimeKind.Utc).AddTicks(9125));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 324, DateTimeKind.Utc).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 324, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 31, 16, 34, 15, 324, DateTimeKind.Utc).AddTicks(207));
        }
    }
}
