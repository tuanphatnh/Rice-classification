using MobileShopAPI.ViewModel;
using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            ProductOrders = new HashSet<ProductOrder>();
            UserRatings = new HashSet<UserRating>();
            MarkedProducts = new HashSet<MarkedProduct>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        /// <summary>
        /// in stock = 0
        /// out of stock = 1
        /// unapproved = 2
        /// soft deleted = 3
        /// User can not modify this value
        /// </summary>
        public int? Status { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        /// <summary>
        /// Product is hidden if true
        /// </summary>
        public bool isHidden { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; } = null!;
        public long SizeId { get; set; }
        public long ColorId { get; set; }
        /// <summary>
        /// Used for sorting algorithms
        /// </summary>
        public int? Priorities { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual Color Color { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }

        public virtual ICollection<MarkedProduct> MarkedProducts { get; set; }
    }
}
