namespace MobileShopAPI.Responses
{
    public class UserManagerResponse
    {

        public string? Message { get; set; }

        public bool isSuccess { get; set; }

        public IEnumerable<String>? Errors { get; set; }

        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
