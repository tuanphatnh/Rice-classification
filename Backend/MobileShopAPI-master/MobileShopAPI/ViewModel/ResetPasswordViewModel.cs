using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
