namespace MobileShopAPI.ViewModel
{
    public class SubscriptionPackageStatusViewModel
    {
        /// <summary>
        ///  0 = inactive
        ///  1 = active (default)
        ///  2 = soft deleted
        /// </summary>
        public int Status { get; set; }
    }
}
