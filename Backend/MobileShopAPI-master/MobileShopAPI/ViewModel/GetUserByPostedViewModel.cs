using MobileShopAPI.Models;

namespace MobileShopAPI.ViewModel
{
    public class GetUserByPostedViewModel
    {
        public ApplicationUser User { get; set;}

        public int Posted { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
