namespace MobileShopAPI.Responses
{
    public class UserRatingResponse
    {
        public float  Rating { get; set; }
        public string? Message { get; set; }

        public bool isSuccess { get; set; }

        public IEnumerable<String>? Errors { get; set; }
    }
}
