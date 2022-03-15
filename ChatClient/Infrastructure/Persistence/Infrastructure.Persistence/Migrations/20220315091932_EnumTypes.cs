using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class EnumTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_AvailabilityStatuses_StatusId",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipChanges_FriendshipStatuses_StatusId",
                table: "FriendshipChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_RedeemTokens_RedeemTokenTypes_TypeId",
                table: "RedeemTokens");

            migrationBuilder.DropTable(
                name: "AvailabilityStatuses");

            migrationBuilder.DropTable(
                name: "FriendshipStatuses");

            migrationBuilder.DropTable(
                name: "RedeemTokenTypes");

            migrationBuilder.DropIndex(
                name: "IX_RedeemTokens_TypeId",
                table: "RedeemTokens");

            migrationBuilder.DropIndex(
                name: "IX_FriendshipChanges_StatusId",
                table: "FriendshipChanges");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_StatusId",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "RedeemTokens",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "FriendshipChanges",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Availabilities",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "RedeemTokens",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FriendshipChanges",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Availabilities",
                newName: "StatusId");

            migrationBuilder.CreateTable(
                name: "AvailabilityStatuses",
                columns: table => new
                {
                    AvailabilityStatusId = table.Column<int>(type: "int", nullable: false),
                    IndicatorColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicatorOverlay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityStatuses", x => x.AvailabilityStatusId);
                });

            migrationBuilder.CreateTable(
                name: "FriendshipStatuses",
                columns: table => new
                {
                    FriendshipStatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipStatuses", x => x.FriendshipStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RedeemTokenTypes",
                columns: table => new
                {
                    RedeemTokenTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedeemTokenTypes", x => x.RedeemTokenTypeId);
                });

            migrationBuilder.InsertData(
                table: "FriendshipStatuses",
                columns: new[] { "FriendshipStatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Accepted" },
                    { 3, "Ignored" },
                    { 4, "Blocked" }
                });

            migrationBuilder.InsertData(
                table: "RedeemTokenTypes",
                columns: new[] { "RedeemTokenTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "EmailConfirmation" },
                    { 2, "PasswordRecovery" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedeemTokens_TypeId",
                table: "RedeemTokens",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipChanges_StatusId",
                table: "FriendshipChanges",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_StatusId",
                table: "Availabilities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RedeemTokenTypes_Name",
                table: "RedeemTokenTypes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_AvailabilityStatuses_StatusId",
                table: "Availabilities",
                column: "StatusId",
                principalTable: "AvailabilityStatuses",
                principalColumn: "AvailabilityStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipChanges_FriendshipStatuses_StatusId",
                table: "FriendshipChanges",
                column: "StatusId",
                principalTable: "FriendshipStatuses",
                principalColumn: "FriendshipStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RedeemTokens_RedeemTokenTypes_TypeId",
                table: "RedeemTokens",
                column: "TypeId",
                principalTable: "RedeemTokenTypes",
                principalColumn: "RedeemTokenTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
