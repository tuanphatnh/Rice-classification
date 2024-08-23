using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class ProductOrder
    {
        public long Id { get; set; }
        public int quantity { get; set; }
        public long ProductId { get; set; }
        public string OrderId { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
