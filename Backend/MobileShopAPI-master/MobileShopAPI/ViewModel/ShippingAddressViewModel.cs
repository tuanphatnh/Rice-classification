using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ShippingAddressViewModel
    {
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string AddressName { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public bool? IsDefault { get; set; }

        [Required]
        public int WardId { get; set; }
        [Required]
        public string AddressDetail { get; set; } = null!;
  
    }
}
