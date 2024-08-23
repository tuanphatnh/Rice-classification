using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class UserRating
    {
        public long Id { get; set; }
        /// <summary>
        /// 1,2,3,4,5
        /// </summary>
        public short Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ProductId { get; set; }
        public string UsderId { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
        public virtual ApplicationUser Usder { get; set; } = null!;
    }
}
