using MobileShopAPI.Models;

namespace MobileShopAPI.ViewModel
{
    public class GetUserByBuyPackageViewModel
    {
        public ApplicationUser User { get; set; }

        public int Purchases { get; set; }

        public IEnumerable<InternalTransaction> internalTransactions { get; set; }
    }
}
