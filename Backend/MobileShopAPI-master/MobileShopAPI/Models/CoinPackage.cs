using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class CoinPackage
    {
        public CoinPackage()
        {
            Transactions = new HashSet<VnpTransaction>();
        }

        public string Id { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public long PackageValue { get; set; }
        /// <summary>
        /// vnđ,...v.v
        /// </summary>
        public string ValueUnit { get; set; } = null!;
        public long? CoinAmount { get; set; }
        public string? Description { get; set; }
        /// <summary>
        ///  0 = inactive 
        ///  1 = active (default)
        ///  2 = soft deleted
        /// </summary>
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<VnpTransaction> Transactions { get; set; }
    }
}
