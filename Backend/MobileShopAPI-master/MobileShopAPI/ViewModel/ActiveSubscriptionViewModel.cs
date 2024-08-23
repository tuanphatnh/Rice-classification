namespace MobileShopAPI.ViewModel
{
    public class ActiveSubscriptionViewModel
    {
        /// <summary>
        /// Số lượng bài đăng đã sử dụng
        /// </summary>
        public int? UsedPost { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public long SpId { get; set; }
    }
}
