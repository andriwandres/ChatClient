namespace Core.Domain.Entities
{
    public class ArchivedRecipient
    {
        public int ArchivedRecipientId { get; set; }
        public int UserId { get; set; }
        public int RecipientId { get; set; }

        public User User { get; set; }
        public Recipient Recipient { get; set; }
    }
}
