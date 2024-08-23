using Microsoft.AspNetCore.Identity;
using MobileShopAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShopAPI.ViewModel
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public string? AvatarUrl { get; set; }
        public long? UserBalance { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public IEnumerable<Product>? Products { get; set; }

    }
}
