using System;
using System.Collections.Generic;

namespace MobileShopAPI.Models
{
    public partial class VnpTransaction
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? VnpVersion { get; set; }
        public string? VnpCommand { get; set; }
        public string? VnpTmnCode { get; set; }
        public long? VnpAmount { get; set; }
        public string? VnpBankCode { get; set; }
        public long? VnpCreateDate { get; set; }
        public string? VnpCurrCode { get; set; }
        public string? VnpIpAddr { get; set; }
        public string? VnpLocale { get; set; }
        public string? VnpOrderInfo { get; set; }
        public string? VnpOrderType { get; set; }
        public string? VnpTxnRef { get; set; }
        public string? VnpSecureHash { get; set; }
        public string? PackageId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual CoinPackage? Package { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
