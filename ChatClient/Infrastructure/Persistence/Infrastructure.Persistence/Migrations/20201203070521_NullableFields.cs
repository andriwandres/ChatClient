using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class NullableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_ParentId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadDate",
                table: "MessageRecipients",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParentId",
                table: "Messages",
                column: "ParentId",
                unique: true,
                filter: "[ParentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_ParentId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadDate",
                table: "MessageRecipients",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParentId",
                table: "Messages",
                column: "ParentId",
                unique: true);
        }
    }
}
