﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Entities.ArchivedRecipient", b =>
                {
                    b.Property<int>("ArchivedRecipientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArchivedRecipientId"), 1L, 1);

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ArchivedRecipientId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("UserId");

                    b.ToTable("ArchivedRecipients");
                });

            modelBuilder.Entity("Core.Domain.Entities.Availability", b =>
                {
                    b.Property<int>("AvailabilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvailabilityId"), 1L, 1);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ModifiedManually")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StatusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AvailabilityId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("Core.Domain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("FlagImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Core.Domain.Entities.DisplayImage", b =>
                {
                    b.Property<int>("DisplayImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DisplayImageId"), 1L, 1);

                    b.Property<byte[]>("Bytes")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("DisplayImageId");

                    b.ToTable("DisplayImages");
                });

            modelBuilder.Entity("Core.Domain.Entities.Friendship", b =>
                {
                    b.Property<int>("FriendshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FriendshipId"), 1L, 1);

                    b.Property<int>("AddresseeId")
                        .HasColumnType("int");

                    b.Property<int>("RequesterId")
                        .HasColumnType("int");

                    b.HasKey("FriendshipId");

                    b.HasIndex("AddresseeId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Core.Domain.Entities.FriendshipChange", b =>
                {
                    b.Property<int>("FriendshipChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FriendshipChangeId"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("FriendshipId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("FriendshipChangeId");

                    b.HasIndex("FriendshipId");

                    b.ToTable("FriendshipChanges");
                });

            modelBuilder.Entity("Core.Domain.Entities.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.HasIndex("GroupImageId")
                        .IsUnique()
                        .HasFilter("[GroupImageId] IS NOT NULL");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Core.Domain.Entities.GroupMembership", b =>
                {
                    b.Property<int>("GroupMembershipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupMembershipId"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GroupMembershipId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMemberships");
                });

            modelBuilder.Entity("Core.Domain.Entities.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("HtmlContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsEdited")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId")
                        .IsUnique()
                        .HasFilter("[ParentId] IS NOT NULL");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Core.Domain.Entities.MessageReaction", b =>
                {
                    b.Property<int>("MessageReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageReactionId"), 1L, 1);

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("ReactionValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MessageReactionId");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageReactions");
                });

            modelBuilder.Entity("Core.Domain.Entities.MessageRecipient", b =>
                {
                    b.Property<int>("MessageRecipientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageRecipientId"), 1L, 1);

                    b.Property<bool>("IsForwarded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.HasKey("MessageRecipientId");

                    b.HasIndex("MessageId");

                    b.HasIndex("RecipientId", "MessageId")
                        .IsUnique();

                    b.ToTable("MessageRecipients");
                });

            modelBuilder.Entity("Core.Domain.Entities.NicknameAssignment", b =>
                {
                    b.Property<int>("NicknameAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NicknameAssignmentId"), 1L, 1);

                    b.Property<int>("AddresseeId")
                        .HasColumnType("int");

                    b.Property<string>("NicknameValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequesterId")
                        .HasColumnType("int");

                    b.HasKey("NicknameAssignmentId");

                    b.HasIndex("AddresseeId");

                    b.HasIndex("RequesterId");

                    b.ToTable("NicknameAssignments");
                });

            modelBuilder.Entity("Core.Domain.Entities.PinnedRecipient", b =>
                {
                    b.Property<int>("PinnedRecipientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PinnedRecipientId"), 1L, 1);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PinnedRecipientId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("UserId");

                    b.ToTable("PinnedRecipients");
                });

            modelBuilder.Entity("Core.Domain.Entities.Recipient", b =>
                {
                    b.Property<int>("RecipientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipientId"), 1L, 1);

                    b.Property<int?>("GroupMembershipId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RecipientId");

                    b.HasIndex("GroupMembershipId")
                        .IsUnique()
                        .HasFilter("[GroupMembershipId] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("Core.Domain.Entities.RedeemToken", b =>
                {
                    b.Property<int>("RedeemTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RedeemTokenId"), 1L, 1);

                    b.Property<bool>("IsUsed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("RedeemTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("RedeemTokens");
                });

            modelBuilder.Entity("Core.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsEmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ProfileImageId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProfileImageId")
                        .IsUnique()
                        .HasFilter("[ProfileImageId] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Domain.Entities.ArchivedRecipient", b =>
                {
                    b.HasOne("Core.Domain.Entities.Recipient", "Recipient")
                        .WithMany("Archives")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("ArchivedRecipients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.Availability", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithOne("Availability")
                        .HasForeignKey("Core.Domain.Entities.Availability", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.Friendship", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "Addressee")
                        .WithMany("AddressedFriendships")
                        .HasForeignKey("AddresseeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "Requester")
                        .WithMany("RequestedFriendships")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Addressee");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("Core.Domain.Entities.FriendshipChange", b =>
                {
                    b.HasOne("Core.Domain.Entities.Friendship", "Friendship")
                        .WithMany("StatusChanges")
                        .HasForeignKey("FriendshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Friendship");
                });

            modelBuilder.Entity("Core.Domain.Entities.Group", b =>
                {
                    b.HasOne("Core.Domain.Entities.DisplayImage", "GroupImage")
                        .WithOne()
                        .HasForeignKey("Core.Domain.Entities.Group", "GroupImageId");

                    b.Navigation("GroupImage");
                });

            modelBuilder.Entity("Core.Domain.Entities.GroupMembership", b =>
                {
                    b.HasOne("Core.Domain.Entities.Group", "Group")
                        .WithMany("Memberships")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("GroupMemberships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.Message", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "Author")
                        .WithMany("AuthoredMessages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.Message", "Parent")
                        .WithOne()
                        .HasForeignKey("Core.Domain.Entities.Message", "ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Author");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Core.Domain.Entities.MessageReaction", b =>
                {
                    b.HasOne("Core.Domain.Entities.Message", "Message")
                        .WithMany("Reactions")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("MessageReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.MessageRecipient", b =>
                {
                    b.HasOne("Core.Domain.Entities.Message", "Message")
                        .WithMany("MessageRecipients")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.Recipient", "Recipient")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("Core.Domain.Entities.NicknameAssignment", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "Addressee")
                        .WithMany("AddressedNicknames")
                        .HasForeignKey("AddresseeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "Requester")
                        .WithMany("RequestedNicknames")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Addressee");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("Core.Domain.Entities.PinnedRecipient", b =>
                {
                    b.HasOne("Core.Domain.Entities.Recipient", "Recipient")
                        .WithMany("Pins")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("PinnedRecipients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.Recipient", b =>
                {
                    b.HasOne("Core.Domain.Entities.GroupMembership", "GroupMembership")
                        .WithOne("Recipient")
                        .HasForeignKey("Core.Domain.Entities.Recipient", "GroupMembershipId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithOne("Recipient")
                        .HasForeignKey("Core.Domain.Entities.Recipient", "UserId");

                    b.Navigation("GroupMembership");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.RedeemToken", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("RedeemTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.User", b =>
                {
                    b.HasOne("Core.Domain.Entities.Country", null)
                        .WithMany("Users")
                        .HasForeignKey("CountryId");

                    b.HasOne("Core.Domain.Entities.DisplayImage", "ProfileImage")
                        .WithOne()
                        .HasForeignKey("Core.Domain.Entities.User", "ProfileImageId");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("Core.Domain.Entities.Country", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Core.Domain.Entities.Friendship", b =>
                {
                    b.Navigation("StatusChanges");
                });

            modelBuilder.Entity("Core.Domain.Entities.Group", b =>
                {
                    b.Navigation("Memberships");
                });

            modelBuilder.Entity("Core.Domain.Entities.GroupMembership", b =>
                {
                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("Core.Domain.Entities.Message", b =>
                {
                    b.Navigation("MessageRecipients");

                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Core.Domain.Entities.Recipient", b =>
                {
                    b.Navigation("Archives");

                    b.Navigation("Pins");

                    b.Navigation("ReceivedMessages");
                });

            modelBuilder.Entity("Core.Domain.Entities.User", b =>
                {
                    b.Navigation("AddressedFriendships");

                    b.Navigation("AddressedNicknames");

                    b.Navigation("ArchivedRecipients");

                    b.Navigation("AuthoredMessages");

                    b.Navigation("Availability");

                    b.Navigation("GroupMemberships");

                    b.Navigation("MessageReactions");

                    b.Navigation("PinnedRecipients");

                    b.Navigation("Recipient");

                    b.Navigation("RedeemTokens");

                    b.Navigation("RequestedFriendships");

                    b.Navigation("RequestedNicknames");
                });
#pragma warning restore 612, 618
        }
    }
}
