using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Entities;
using Xunit;

namespace Core.Domain.Test.Entities
{
    public class UserTests
    {
        [Fact]
        public void UserConstructor_ShouldInitializeRedeemTokensNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.RedeemTokens);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeArchivedRecipientsNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.ArchivedRecipients);
        }

        [Fact]
        public void UserConstructor_ShouldInitializePinnedRecipientsNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.PinnedRecipients);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeAddressedFriendshipsNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.AddressedFriendships);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeRequestedFriendshipsNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.RequestedFriendships);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeGroupMembershipsNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.GroupMemberships);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeAuthoredMessagesNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.AuthoredMessages);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeMessageReactionsNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.MessageReactions);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeAddressedNicknamesNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.AddressedNicknames);
        }

        [Fact]
        public void UserConstructor_ShouldInitializeRequestedNicknamesNavigationProperty_WithEmptyCollection()
        {
            // Act
            User user = new User();

            // Assert
            Assert.Empty(user.RequestedNicknames);
        }
    }
}
