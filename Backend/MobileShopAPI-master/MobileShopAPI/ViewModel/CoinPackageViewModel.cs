namespace MobileShopAPI.ViewModel
{
    public class CoinPackageViewModel
    {
        public string PackageName { get; set; } = null!;
        public long PackageValue { get; set; }
        /// <summary>
        /// vnđ,...v.v
        /// </summary>
        public string ValueUnit { get; set; } = null!;
        public long? CoinAmount { get; set; }
        public string? Description { get; set; }
    }
}
