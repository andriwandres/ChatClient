namespace Core.Domain.Entities
{
    public class StatusMessage
    {
        public int StatusMessageId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }

        public User User { get; set; }
    }
}
