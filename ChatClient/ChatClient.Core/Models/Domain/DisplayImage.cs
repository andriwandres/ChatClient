
namespace ChatClient.Core.Models.Domain
{
    public class DisplayImage
    {
        public int DisplayImageId { get; set; }
        public byte[] Image { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
