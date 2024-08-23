using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
            Transactions = new HashSet<VnpTransaction>();
        }

        public string Id { get; set; } = null!;
        public long Total { get; set; }
        /// <summary>
        /// 0 = pending (default)
        /// 1 = completed
        /// </summary>
        public int? Status { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        /// <summary>
        /// 1 = trả hết, 2 = đặt cọc
        /// </summary>
        public int? Type { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<VnpTransaction> Transactions { get; set; }
    }
}
