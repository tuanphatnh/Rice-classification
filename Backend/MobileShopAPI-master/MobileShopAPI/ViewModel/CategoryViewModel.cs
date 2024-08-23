using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        /// <summary>
        /// url hình ảnh
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}
