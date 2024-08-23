namespace MobileShopAPI.Request
{
    public class ProductCreateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public int? Status { get; set; }
        public string UserId { get; set; } = null!;
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long SizeId { get; set; }
        /// <summary>
        /// part of primaryKey
        /// </summary>
        public long ColorId { get; set; }
    }
}
