using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Emoji
    {
        public int EmojiId { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string Shortcut { get; set; }

        public ICollection<MessageReaction> Reactions { get; set; }

        public Emoji()
        {
            Reactions = new HashSet<MessageReaction>();
        }
    }
}
