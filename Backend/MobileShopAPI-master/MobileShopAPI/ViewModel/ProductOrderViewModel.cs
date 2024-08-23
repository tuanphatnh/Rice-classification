using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ProductOrderViewModel
    {
        [Required]
        public long ProductId { get; set; }
        [Required]
        public string OrderId { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
    }
}
