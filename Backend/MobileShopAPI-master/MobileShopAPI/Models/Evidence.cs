using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class Evidence
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public long ReportId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Report Report { get; set; } = null!;
    }
}
