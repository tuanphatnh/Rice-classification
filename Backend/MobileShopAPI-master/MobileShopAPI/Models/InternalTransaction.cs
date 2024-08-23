using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class InternalTransaction
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public long? CoinActionId { get; set; }
        public long? SpId { get; set; }
        public int? ItAmount { get; set; }
        public string? ItInfo { get; set; }
        public DateTime? ItSecureHash { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual CoinAction? CoinAction { get; set; }
        public virtual SubscriptionPackage? Sp { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
