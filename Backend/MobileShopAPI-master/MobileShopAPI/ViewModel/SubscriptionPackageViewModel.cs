using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class SubscriptionPackageViewModel
    {
        [Required]
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
    }
}
