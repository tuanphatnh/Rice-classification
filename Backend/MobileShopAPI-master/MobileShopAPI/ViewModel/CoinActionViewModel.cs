using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class CoinActionViewModel
    {
        [Required]
        public string ActionName { get; set; } = null!;
        public string? Description { get; set; }

        /// <summary>
        /// <example> 10000 or 20000 ....</example>
        /// </summary>
        public int? CaCoinAmount { get; set; }

        public int Status { get; set; }
    }
}
