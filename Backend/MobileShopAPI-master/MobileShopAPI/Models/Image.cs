using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Image
    {
        public long Id { get; set; }
        /// <summary>
        /// url hình ảnh
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// hình ảnh là ảnh bìa
        /// </summary>
        public bool IsCover { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ProductId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool IsVideo { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
