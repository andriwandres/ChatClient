using ChatClient.Core.Models.Domain;

namespace ChatClient.Core.Models.ViewModels.Contact
{
    public class ContactViewModel
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string DisplayName { get; set; }
        public UserRelationshipStatus Status { get; set; }
    }
}
