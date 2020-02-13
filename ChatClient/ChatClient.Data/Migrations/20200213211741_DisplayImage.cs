using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatClient.Data.Migrations
{
    public partial class DisplayImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupImageId",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DisplayImage",
                columns: table => new
                {
                    DisplayImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayImage", x => x.DisplayImageId);
                });

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 517, DateTimeKind.Utc).AddTicks(5934));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 517, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 517, DateTimeKind.Utc).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 517, DateTimeKind.Utc).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 517, DateTimeKind.Utc).AddTicks(7149));

            migrationBuilder.UpdateData(
                table: "GroupMemberships",
                keyColumn: "GroupMembershipId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 517, DateTimeKind.Utc).AddTicks(7152));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 515, DateTimeKind.Utc).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 515, DateTimeKind.Utc).AddTicks(2048));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 1,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(3991));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 2,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5488));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 3,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5547));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 4,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5551));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 5,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 6,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 7,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 8,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5578));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 9,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5585));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 10,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5588));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 11,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5591));

            migrationBuilder.UpdateData(
                table: "MessageRecipients",
                keyColumn: "MessageRecipientId",
                keyValue: 12,
                column: "ReadAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 519, DateTimeKind.Utc).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(3611));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(6019));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(6082));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(6170));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 516, DateTimeKind.Utc).AddTicks(6173));

            migrationBuilder.UpdateData(
                table: "UserRelationships",
                keyColumn: "UserRelationshipId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 520, DateTimeKind.Utc).AddTicks(4719));

            migrationBuilder.UpdateData(
                table: "UserRelationships",
                keyColumn: "UserRelationshipId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 520, DateTimeKind.Utc).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "UserRelationships",
                keyColumn: "UserRelationshipId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 520, DateTimeKind.Utc).AddTicks(5941));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 512, DateTimeKind.Utc).AddTicks(5514));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 512, DateTimeKind.Utc).AddTicks(6752));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 512, DateTimeKind.Utc).AddTicks(6778));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 13, 21, 17, 40, 512, DateTimeKind.Utc).AddTicks(6784));

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                unique: true,
                filter: "[ProfileImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupImageId",
                table: "Groups",
                column: "GroupImageId",
                unique: true,
                filter: "[GroupImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DisplayImage_GroupImageId",
                table: "Groups",
                column: "GroupImageId",
                principalTable: "DisplayImage",
                principalColumn: "DisplayImageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DisplayImage_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                principalTable: "DisplayImage",
                principalColumn: "DisplayImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DisplayImage_GroupImageId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_DisplayImage_ProfileImageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DisplayImage");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupImageId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupImageId",
                table: "Groups");

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

            migrationBuilder.UpdateData(
                table: "UserRelationships",
                keyColumn: "UserRelationshipId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 811, DateTimeKind.Utc).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "UserRelationships",
                keyColumn: "UserRelationshipId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 811, DateTimeKind.Utc).AddTicks(3444));

            migrationBuilder.UpdateData(
                table: "UserRelationships",
                keyColumn: "UserRelationshipId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 4, 16, 49, 48, 811, DateTimeKind.Utc).AddTicks(3472));

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
        }
    }
}
