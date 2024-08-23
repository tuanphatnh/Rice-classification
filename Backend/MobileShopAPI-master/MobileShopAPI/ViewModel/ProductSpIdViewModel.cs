using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ProductSpIdViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public long Price { get; set; }
        /// <summary>
        /// in stock = 0
        /// out of stock = 1
        /// unapproved = 2
        /// soft deleted = 3
        /// User can not modify this value
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Product is hidden if true
        /// </summary>
        
        public long SpId { get; set; }
        public bool isHidden { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public long SizeId { get; set; }
        [Required]
        public long ColorId { get; set; }
        /// <summary>
        /// Images list of this product
        /// </summary>
        public List<ImageViewModel>? Images { get; set; }

    }
}
