namespace MobileShopAPI.Models
{
    public class MarkedProduct
    {
        public long Id { get; set; }

        public string UserId { get; set; } = null!;
        public long ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
