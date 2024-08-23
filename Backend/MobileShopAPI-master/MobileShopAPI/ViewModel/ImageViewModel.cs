namespace MobileShopAPI.ViewModel
{
    public class ImageViewModel
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

        /// <summary>
        /// Đây là video
        /// </summary>
        public bool IsVideo { get; set; }

        /// <summary>
        /// Biến này chỉ được sử dụng khi chỉnh sửa sản phẩm
        /// Nếu đặt là true, hình ảnh này sẽ bị xóa
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Biến này chỉ được sử dụng khi chỉnh sửa sản phẩm
        /// Đặt là true để đánh dấu hình ảnh được thêm mới
        /// </summary>
        public bool IsNewlyAdded { get; set; }
    }
}
