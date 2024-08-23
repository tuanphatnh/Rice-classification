using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ColorViewModel
    {
        [Required]
        public string ColorName { get; set; } = null!;

        /// <summary>
        /// Hex (Hexadecimal) is one of the methods of color definition
        /// </summary>
        [Required]
        public string HexValue { get; set; } = null!;
    }
}
