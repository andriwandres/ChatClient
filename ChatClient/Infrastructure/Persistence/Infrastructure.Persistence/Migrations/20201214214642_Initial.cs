using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailabilityStatuses",
                columns: table => new
                {
                    AvailabilityStatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IndicatorColor = table.Column<string>(nullable: false),
                    IndicatorOverlay = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityStatuses", x => x.AvailabilityStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    FlagImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "DisplayImages",
                columns: table => new
                {
                    DisplayImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayImages", x => x.DisplayImageId);
                });

            migrationBuilder.CreateTable(
                name: "Emojis",
                columns: table => new
                {
                    EmojiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: false),
                    Shortcut = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emojis", x => x.EmojiId);
                });

            migrationBuilder.CreateTable(
                name: "FriendshipStatuses",
                columns: table => new
                {
                    FriendshipStatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipStatuses", x => x.FriendshipStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RedeemTokenTypes",
                columns: table => new
                {
                    RedeemTokenTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedeemTokenTypes", x => x.RedeemTokenTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(nullable: false)
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
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupImageId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_DisplayImages_GroupImageId",
                        column: x => x.GroupImageId,
                        principalTable: "DisplayImages",
                        principalColumn: "DisplayImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileImageId = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    IsEmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_DisplayImages_ProfileImageId",
                        column: x => x.ProfileImageId,
                        principalTable: "DisplayImages",
                        principalColumn: "DisplayImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    AvailabilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedManually = table.Column<bool>(nullable: false, defaultValue: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.AvailabilityId);
                    table.ForeignKey(
                        name: "FK_Availabilities_AvailabilityStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "AvailabilityStatuses",
                        principalColumn: "AvailabilityStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Availabilities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterId = table.Column<int>(nullable: false),
                    AddresseeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_AddresseeId",
                        column: x => x.AddresseeId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "GroupMemberships",
                columns: table => new
                {
                    GroupMembershipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMemberships", x => x.GroupMembershipId);
                    table.ForeignKey(
                        name: "FK_GroupMemberships_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMemberships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    HtmlContent = table.Column<string>(nullable: false),
                    IsEdited = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Messages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Messages",
                        principalColumn: "MessageId");
                });

            migrationBuilder.CreateTable(
                name: "NicknameAssignments",
                columns: table => new
                {
                    NicknameAssignmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterId = table.Column<int>(nullable: false),
                    AddresseeId = table.Column<int>(nullable: false),
                    NicknameValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NicknameAssignments", x => x.NicknameAssignmentId);
                    table.ForeignKey(
                        name: "FK_NicknameAssignments_Users_AddresseeId",
                        column: x => x.AddresseeId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NicknameAssignments_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RedeemTokens",
                columns: table => new
                {
                    RedeemTokenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Token = table.Column<Guid>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false, defaultValue: false),
                    ValidUntil = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedeemTokens", x => x.RedeemTokenId);
                    table.ForeignKey(
                        name: "FK_RedeemTokens_RedeemTokenTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "RedeemTokenTypes",
                        principalColumn: "RedeemTokenTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RedeemTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusMessages",
                columns: table => new
                {
                    StatusMessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
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
                name: "FriendshipChanges",
                columns: table => new
                {
                    FriendshipChangeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendshipId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipChanges", x => x.FriendshipChangeId);
                    table.ForeignKey(
                        name: "FK_FriendshipChanges_Friendships_FriendshipId",
                        column: x => x.FriendshipId,
                        principalTable: "Friendships",
                        principalColumn: "FriendshipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendshipChanges_FriendshipStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "FriendshipStatuses",
                        principalColumn: "FriendshipStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    RecipientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupMembershipId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_Recipients_GroupMemberships_GroupMembershipId",
                        column: x => x.GroupMembershipId,
                        principalTable: "GroupMemberships",
                        principalColumn: "GroupMembershipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageReactions",
                columns: table => new
                {
                    MessageReactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false),
                    EmojiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReactions", x => x.MessageReactionId);
                    table.ForeignKey(
                        name: "FK_MessageReactions_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "EmojiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReactions_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ArchivedRecipients",
                columns: table => new
                {
                    ArchivedRecipientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedRecipients", x => x.ArchivedRecipientId);
                    table.ForeignKey(
                        name: "FK_ArchivedRecipients_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchivedRecipients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MessageRecipients",
                columns: table => new
                {
                    MessageRecipientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false),
                    IsForwarded = table.Column<bool>(nullable: false, defaultValue: false),
                    IsRead = table.Column<bool>(nullable: false, defaultValue: false),
                    ReadDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageRecipients", x => x.MessageRecipientId);
                    table.ForeignKey(
                        name: "FK_MessageRecipients_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageRecipients_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId");
                });

            migrationBuilder.CreateTable(
                name: "PinnedRecipients",
                columns: table => new
                {
                    PinnedRecipientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinnedRecipients", x => x.PinnedRecipientId);
                    table.ForeignKey(
                        name: "FK_PinnedRecipients_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PinnedRecipients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
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
                name: "IX_ArchivedRecipients_RecipientId",
                table: "ArchivedRecipients",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedRecipients_UserId_RecipientId",
                table: "ArchivedRecipients",
                columns: new[] { "UserId", "RecipientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_StatusId",
                table: "Availabilities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_UserId",
                table: "Availabilities",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipChanges_FriendshipId",
                table: "FriendshipChanges",
                column: "FriendshipId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipChanges_StatusId",
                table: "FriendshipChanges",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_AddresseeId",
                table: "Friendships",
                column: "AddresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId_AddresseeId",
                table: "Friendships",
                columns: new[] { "RequesterId", "AddresseeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_GroupId",
                table: "GroupMemberships",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_UserId_GroupId",
                table: "GroupMemberships",
                columns: new[] { "UserId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupImageId",
                table: "Groups",
                column: "GroupImageId",
                unique: true,
                filter: "[GroupImageId] IS NOT NULL");

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
                name: "IX_MessageReactions_EmojiId",
                table: "MessageReactions",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_MessageId",
                table: "MessageReactions",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_UserId_MessageId_EmojiId",
                table: "MessageReactions",
                columns: new[] { "UserId", "MessageId", "EmojiId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecipients_MessageId",
                table: "MessageRecipients",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecipients_RecipientId_MessageId",
                table: "MessageRecipients",
                columns: new[] { "RecipientId", "MessageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParentId",
                table: "Messages",
                column: "ParentId",
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NicknameAssignments_AddresseeId",
                table: "NicknameAssignments",
                column: "AddresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_NicknameAssignments_RequesterId_AddresseeId",
                table: "NicknameAssignments",
                columns: new[] { "RequesterId", "AddresseeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PinnedRecipients_RecipientId",
                table: "PinnedRecipients",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedRecipients_UserId_RecipientId",
                table: "PinnedRecipients",
                columns: new[] { "UserId", "RecipientId" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_RedeemTokens_TypeId",
                table: "RedeemTokens",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RedeemTokens_UserId",
                table: "RedeemTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RedeemTokenTypes_Name",
                table: "RedeemTokenTypes",
                column: "Name",
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                unique: true,
                filter: "[ProfileImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedRecipients");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "FriendshipChanges");

            migrationBuilder.DropTable(
                name: "MessageReactions");

            migrationBuilder.DropTable(
                name: "MessageRecipients");

            migrationBuilder.DropTable(
                name: "NicknameAssignments");

            migrationBuilder.DropTable(
                name: "PinnedRecipients");

            migrationBuilder.DropTable(
                name: "RedeemTokens");

            migrationBuilder.DropTable(
                name: "StatusMessages");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "AvailabilityStatuses");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "FriendshipStatuses");

            migrationBuilder.DropTable(
                name: "Emojis");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "RedeemTokenTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "GroupMemberships");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DisplayImages");
        }
    }
}
