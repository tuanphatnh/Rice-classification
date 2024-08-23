using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IUserManagerService
    {
        Task<List<UserViewModel>> GetAllUserAsync();
        Task<UserViewModel?> GetUserProfileAsync(string userId);

        Task<UserViewModel?> GetUserOwnProfileAsync(string userId);

        Task<UserViewModel?> UpdateUserProfileAsync(string userId,UpdateUserProfileViewModel model);

        Task<UserManagerResponse?> BanUser(string userId);

    }

    public class UserManagerService:IUserManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserManagerResponse?> BanUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    isSuccess = false
                };

            user.Status = 1;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new UserManagerResponse
            {
                Message = "User has been banned",
                isSuccess = true
            };
        }
        public async Task<List<UserViewModel>> GetAllUserAsync()
        {
            var userList = await _context.Users.ToListAsync();

            var userViewList = new List<UserViewModel>();

            foreach(var user in userList)
            {
                var temp = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.LastName,
                    LastName = user.LastName,
                    Description = user.Description,
                    Status = user.Status,
                    AvatarUrl = user.AvatarUrl,
                    UserBalance = user.UserBalance,
                    CreatedDate = user.CreatedDate,
                    UpdatedDate = user.UpdatedDate
                };
                userViewList.Add(temp);
            }


            return userViewList;
        }

        public async Task<UserViewModel?> GetUserOwnProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return null; }
            var productList = await _context.Products.AsNoTracking()
                .Include(p=>p.Images)
                .Where(p =>p.UserId == userId)
                .ToListAsync();


            var userView = new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.LastName,
                LastName = user.LastName,
                Description = user.Description,
                Status = user.Status,
                AvatarUrl = user.AvatarUrl,
                UserBalance = user.UserBalance,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                Products = productList
            };
            return userView;
        }

        public async Task<UserViewModel?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return null; }
            var productList = await _context.Products.AsNoTracking().Include(p => p.Images).Where(p=>
                p.UserId == userId && 
                p.isHidden == false &&
                (p.Status == 0 || p.Status == 1))
                .ToListAsync();


            var userView = new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.LastName,
                LastName = user.LastName,
                Description = user.Description,
                Status = user.Status,
                AvatarUrl = user.AvatarUrl,
                UserBalance = user.UserBalance,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                Products = productList
            };
            return userView;
        }

        public async Task<UserViewModel?> UpdateUserProfileAsync(string userId, UpdateUserProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.MiddleName = model.MiddleName;
            user.AvatarUrl = model.AvatarUrl;
            user.Description = model.Description;
            user.PhoneNumber = model.PhoneNumber;
            user.UpdatedDate = DateTime.Now;
            user.UserBalance = model.UserBalance;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.LastName,
                LastName = user.LastName,
                Description = user.Description,
                Status = user.Status,
                AvatarUrl = user.AvatarUrl,
                UserBalance = user.UserBalance,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }

    }
}
