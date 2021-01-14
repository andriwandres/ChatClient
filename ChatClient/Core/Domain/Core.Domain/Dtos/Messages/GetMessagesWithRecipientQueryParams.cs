using System;

namespace Core.Domain.Dtos.Messages
{
    public class GetMessagesWithRecipientQueryParams
    {
        public int Limit { get; set; }
        public DateTime LowerBound { get; set; }
    }
}
