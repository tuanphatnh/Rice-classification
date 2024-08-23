using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class ResetEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
