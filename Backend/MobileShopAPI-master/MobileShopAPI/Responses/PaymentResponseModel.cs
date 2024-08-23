namespace MobileShopAPI.Responses
{
    public class PaymentResponseModel
    {
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public string? OrderId { get; set; }
        public string packageId { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
