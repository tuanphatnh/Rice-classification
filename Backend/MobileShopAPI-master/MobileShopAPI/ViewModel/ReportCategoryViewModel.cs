using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ReportCategoryViewModel
    {
        /// <summary>
        /// Name of report category
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Description of report category
        /// </summary>
        public string? Description { get; set; }
    }
}
