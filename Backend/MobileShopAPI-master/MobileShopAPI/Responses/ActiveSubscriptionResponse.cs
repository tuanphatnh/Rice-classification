namespace MobileShopAPI.Responses
{
    public class ActiveSubscriptionResponse
    {
        public string? Message { get; set; }

        public bool isSuccess { get; set; }

        public IEnumerable<String>? Errors { get; set; }
    }
}
