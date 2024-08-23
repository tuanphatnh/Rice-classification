using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class GetPostsByUserViewModel
    {
        public string User { get; set; }

        public int Posted { get; set; }
    }
}
