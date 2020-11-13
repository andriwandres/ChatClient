using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class RedeemTokenType
    {
        public RedeemTokenTypeId RedeemTokenTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<RedeemToken> Tokens { get; set; }
    }

    public enum RedeemTokenTypeId
    {
        EmailConfirmation = 1,
        PasswordRecovery = 2,
    }
}
