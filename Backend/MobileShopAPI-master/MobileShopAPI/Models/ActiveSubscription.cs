using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class ActiveSubscription
    {
        public long Id { get; set; }
        /// <summary>
        /// Số lượng bài đăng đã sử dụng
        /// </summary>
        public int? UsedPost { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public string UserId { get; set; } = null!;
        public long SpId { get; set; }

        public virtual SubscriptionPackage Sp { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
