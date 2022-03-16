using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId_GroupMembershipId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_PinnedRecipients_UserId_RecipientId",
                table: "PinnedRecipients");

            migrationBuilder.DropIndex(
                name: "IX_NicknameAssignments_RequesterId_AddresseeId",
                table: "NicknameAssignments");

            migrationBuilder.DropIndex(
                name: "IX_MessageReactions_UserId_MessageId_ReactionValue",
                table: "MessageReactions");

            migrationBuilder.DropIndex(
                name: "IX_GroupMemberships_UserId_GroupId",
                table: "GroupMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_RequesterId_AddresseeId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Code",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_ArchivedRecipients_UserId_RecipientId",
                table: "ArchivedRecipients");

            migrationBuilder.AlterColumn<string>(
                name: "ReactionValue",
                table: "MessageReactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PinnedRecipients_UserId",
                table: "PinnedRecipients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NicknameAssignments_RequesterId",
                table: "NicknameAssignments",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_UserId",
                table: "MessageReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_UserId",
                table: "GroupMemberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedRecipients_UserId",
                table: "ArchivedRecipients",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PinnedRecipients_UserId",
                table: "PinnedRecipients");

            migrationBuilder.DropIndex(
                name: "IX_NicknameAssignments_RequesterId",
                table: "NicknameAssignments");

            migrationBuilder.DropIndex(
                name: "IX_MessageReactions_UserId",
                table: "MessageReactions");

            migrationBuilder.DropIndex(
                name: "IX_GroupMemberships_UserId",
                table: "GroupMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_ArchivedRecipients_UserId",
                table: "ArchivedRecipients");

            migrationBuilder.AlterColumn<string>(
                name: "ReactionValue",
                table: "MessageReactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId_GroupMembershipId",
                table: "Recipients",
                columns: new[] { "UserId", "GroupMembershipId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [GroupMembershipId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedRecipients_UserId_RecipientId",
                table: "PinnedRecipients",
                columns: new[] { "UserId", "RecipientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NicknameAssignments_RequesterId_AddresseeId",
                table: "NicknameAssignments",
                columns: new[] { "RequesterId", "AddresseeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_UserId_MessageId_ReactionValue",
                table: "MessageReactions",
                columns: new[] { "UserId", "MessageId", "ReactionValue" },
                unique: true,
                filter: "[ReactionValue] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_UserId_GroupId",
                table: "GroupMemberships",
                columns: new[] { "UserId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId_AddresseeId",
                table: "Friendships",
                columns: new[] { "RequesterId", "AddresseeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedRecipients_UserId_RecipientId",
                table: "ArchivedRecipients",
                columns: new[] { "UserId", "RecipientId" },
                unique: true);
        }
    }
}
