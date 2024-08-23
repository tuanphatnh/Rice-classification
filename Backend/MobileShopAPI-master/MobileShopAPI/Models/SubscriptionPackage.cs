using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class SubscriptionPackage
    {
        public SubscriptionPackage()
        {
            ActiveSubscriptions = new HashSet<ActiveSubscription>();
            InternalTransactions = new HashSet<InternalTransaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public long Price { get; set; }
        /// <summary>
        /// Số lượng được tin đăng khi mua gói
        /// </summary>
        public int PostAmout { get; set; }
        /// <summary>
        /// Số ngày sử dụng
        /// </summary>
        public int ExpiredIn { get; set; }
        /// <summary>
        ///  0 = inactive
        ///  1 = active (default)
        ///  2 = soft deleted
        /// </summary>
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ActiveSubscription> ActiveSubscriptions { get; set; }
        public virtual ICollection<InternalTransaction> InternalTransactions { get; set; }
    }
}
