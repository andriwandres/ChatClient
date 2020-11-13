using System;

namespace Core.Domain.Entities
{
    public class RedeemToken
    {
        public int RedeemTokenId { get; set; }
        public int UserId { get; set; }
        public RedeemTokenTypeId TypeId { get; set; }
        public Guid Token { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ValidUntil { get; set; }

        public User User { get; set; }
        public RedeemTokenType Type { get; set; }
    }
}
