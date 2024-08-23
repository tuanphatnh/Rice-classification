using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Report
    {
        public Report()
        {
            Evidences = new HashSet<Evidence>();
        }

        public long Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        /// <summary>
        /// 0 = pending (default)
        /// 1 = solved
        /// 2 = soft deleted
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// UserId of user who being reported
        /// </summary>
        public string ReportedUserId { get; set; } = null!;
        /// <summary>
        /// ProductId of product that being reported
        /// </summary>
        public long? ReportedProductId { get; set; }
        /// <summary>
        /// user id of user who sent the report
        /// </summary>
        public string UserId { get; set; } = null!;
        public long ReportCategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ReportCategory ReportCategory { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}
