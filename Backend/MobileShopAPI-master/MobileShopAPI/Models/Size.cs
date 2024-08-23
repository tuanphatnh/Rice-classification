using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Size
    {
        public Size()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string SizeName { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
