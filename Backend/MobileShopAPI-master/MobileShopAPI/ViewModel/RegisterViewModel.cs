using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
