using MobileShopAPI.Models;

namespace MobileShopAPI.ViewModel
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; } = null!;
        public UserViewModel User { get; set; } = null!;
    }
}
