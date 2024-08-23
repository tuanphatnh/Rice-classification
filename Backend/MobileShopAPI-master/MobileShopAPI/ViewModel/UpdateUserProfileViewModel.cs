using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MobileShopAPI.ViewModel
{
    public class UpdateUserProfileViewModel
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Description { get; set; }

        public long UserBalance { get; set; }
        public string? PhoneNumber { get; set;}
    }
}
