using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class SizeViewModel
    {
        [Required]
        public string SizeName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
