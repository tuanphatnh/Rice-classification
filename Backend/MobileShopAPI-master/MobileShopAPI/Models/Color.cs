using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Color
    {
        public Color()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string ColorName { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Hex (Hexadecimal) is one of the methods of color definition
        /// </summary>
        public string HexValue { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
