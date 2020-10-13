using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ChatClient.Tests.TestUtilities
{
    public class DataContainer
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<MessageRecipient> MessageRecipients { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<GroupMembership> GroupMemberships { get; set; }
        public IEnumerable<UserRelationship> UserRelationships { get; set; }

        public DataContainer()
        {
            Users = Enumerable.Empty<User>();
            Groups = Enumerable.Empty<Group>();
            Messages = Enumerable.Empty<Message>();
            UserRelationships = Enumerable.Empty<UserRelationship>();
            MessageRecipients = Enumerable.Empty<MessageRecipient>();
            GroupMemberships = Enumerable.Empty<GroupMembership>();
        }
    }
}
