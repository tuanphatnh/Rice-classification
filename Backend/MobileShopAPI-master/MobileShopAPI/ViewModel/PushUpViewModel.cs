using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class PushUpViewModel
    {
        [Required]
        public long SpId { get; set; }
    }
}
