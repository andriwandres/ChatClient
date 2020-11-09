namespace Core.Domain.Entities
{
    public class MessageReaction
    {
        public int MessageReactionId { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public int EmojiId { get; set; }

        public User User { get; set; }
        public Message Message { get; set; }
        public Emoji Emoji { get; set; }
    }
}
