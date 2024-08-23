namespace MobileShopAPI.ViewModel
{
    public class UserRatingViewModel
    {
        /// <summary>
        /// 1, 2, 3, 4, 5
        /// </summary>
        public short Rating { get; set; }
        public string? Comment { get; set; }
        public long ProductId { get; set; }
    }
}
