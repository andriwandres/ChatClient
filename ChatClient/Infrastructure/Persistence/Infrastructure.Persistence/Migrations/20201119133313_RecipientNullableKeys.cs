using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class RecipientNullableKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipients_GroupMembershipId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId_GroupMembershipId",
                table: "Recipients");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Recipients",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GroupMembershipId",
                table: "Recipients",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_GroupMembershipId",
                table: "Recipients",
                column: "GroupMembershipId",
                unique: true,
                filter: "[GroupMembershipId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId_GroupMembershipId",
                table: "Recipients",
                columns: new[] { "UserId", "GroupMembershipId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [GroupMembershipId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipients_GroupMembershipId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId_GroupMembershipId",
                table: "Recipients");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Recipients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupMembershipId",
                table: "Recipients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_GroupMembershipId",
                table: "Recipients",
                column: "GroupMembershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId_GroupMembershipId",
                table: "Recipients",
                columns: new[] { "UserId", "GroupMembershipId" },
                unique: true);
        }
    }
}
