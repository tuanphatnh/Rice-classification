using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class OrderUpdateViewModel
    {
        
        [Required]
        public long Total { get; set; }
        public int? Status { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string UserFullName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        /// <summary>
        /// 1 = trả hết, 2 = đặt cọc
        /// </summary>
        public int? Type { get; set; }

        public List<ProductOrderViewModel>? ProductOrder { get; set; }
    }
}
