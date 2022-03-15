using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveObsoleteTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageReactions_Emojis_EmojiId",
                table: "MessageReactions");

            migrationBuilder.DropTable(
                name: "Emojis");

            migrationBuilder.DropTable(
                name: "StatusMessages");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_MessageReactions_EmojiId",
                table: "MessageReactions");

            migrationBuilder.DropIndex(
                name: "IX_MessageReactions_UserId_MessageId_EmojiId",
                table: "MessageReactions");

            migrationBuilder.DropColumn(
                name: "EmojiId",
                table: "MessageReactions");

            migrationBuilder.AddColumn<string>(
                name: "ReactionValue",
                table: "MessageReactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "Availabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_UserId_MessageId_ReactionValue",
                table: "MessageReactions",
                columns: new[] { "UserId", "MessageId", "ReactionValue" },
                unique: true,
                filter: "[ReactionValue] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageReactions_UserId_MessageId_ReactionValue",
                table: "MessageReactions");

            migrationBuilder.DropColumn(
                name: "ReactionValue",
                table: "MessageReactions");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "Availabilities");

            migrationBuilder.AddColumn<int>(
                name: "EmojiId",
                table: "MessageReactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Emojis",
                columns: table => new
                {
                    EmojiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shortcut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emojis", x => x.EmojiId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_Languages_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusMessages",
                columns: table => new
                {
                    StatusMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMessages", x => x.StatusMessageId);
                    table.ForeignKey(
                        name: "FK_StatusMessages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    TranslationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.TranslationId);
                    table.ForeignKey(
                        name: "FK_Translations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_EmojiId",
                table: "MessageReactions",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_UserId_MessageId_EmojiId",
                table: "MessageReactions",
                columns: new[] { "UserId", "MessageId", "EmojiId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CountryId",
                table: "Languages",
                column: "CountryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusMessages_UserId",
                table: "StatusMessages",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LanguageId_Key",
                table: "Translations",
                columns: new[] { "LanguageId", "Key" });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReactions_Emojis_EmojiId",
                table: "MessageReactions",
                column: "EmojiId",
                principalTable: "Emojis",
                principalColumn: "EmojiId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
