using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ReportViewModel
    {
        [Required]
        public string Subject { get; set; } = null!;// tieu de

        [Required]
        public string Body { get; set; } = null!;

        [Required]
        public string ReportedUserId { get; set; } = null!;

        /// <summary>
        /// ProductId of product that being reported
        /// </summary>
        [Required] 
        public long ReportedProductId { get; set; }

        /// <summary>
        /// user id of user who sent the report
        /// </summary>
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public long ReportCategoryId { get; set; }

        public List<EvidenceViewModel>? Evidences { get; set; }
    }
}
