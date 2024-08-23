using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class ShippingAddress
    {
        public long Id { get; set; }
        public string UserId { get; set; } = null!;
        public string AddressName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool? IsDefault { get; set; }
        public int WardId { get; set; }
        /// <summary>
        /// house number, district name
        /// </summary>
        public string AddressDetail { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
