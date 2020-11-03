using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class RedeemTokenType
    {
        public int RedeemTokenTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<RedeemToken> Tokens { get; set; }
    }
}
