namespace MobileShopAPI.Responses
{
    public class CoinActionResponse
    {
        public string? Message { get; set; }

        public bool isSuccess { get; set; }

        public IEnumerable<String>? Errors { get; set; }
    }
}
