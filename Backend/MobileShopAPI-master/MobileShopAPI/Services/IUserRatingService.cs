using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface IUserRatingService
    {
        Task<List<UserRating>> GetAllAsync();

        Task<List<UserRating>> GetByIdAsync(long id);

        Task<List<UserRating>> GetAllRatingOfUserAsync(string id);

        Task<UserRatingResponse> AddAsync(string UsrId, UserRatingViewModel ur);

        Task<UserRatingResponse> UpdateAsync(long id, UserRatingViewModel ur);

        Task<UserRatingResponse> DeleteAsync(long id);

        Task<float> getAverageRatingAsync(long id);
    }

    public class UserRatingService : IUserRatingService
    {
        private readonly ApplicationDbContext _context;

        public UserRatingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserRatingResponse> AddAsync(string UsrId, UserRatingViewModel ur)
        {
            var _userRating = new UserRating
            {
                Rating = ur.Rating,
                Comment = ur.Comment,
                CreatedDate = null,
                ProductId = ur.ProductId,
                UsderId = UsrId
            };
            _context.Add(_userRating);
            await _context.SaveChangesAsync();

            return new UserRatingResponse
            {
                Rating = ur.Rating, 
                Message = "New User Rating Added!",
                isSuccess = true
            };
        }

        public async Task<UserRatingResponse> DeleteAsync(long id)
        {
            var ur = await _context.UserRatings.SingleOrDefaultAsync(br => br.Id == id);
            if (ur != null)
            {
                _context.Remove(ur);
                await _context.SaveChangesAsync();

                return new UserRatingResponse
                {
                    Message = "This User Rating Deleted",
                    isSuccess = true
                };
            }

            return new UserRatingResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<UserRating>> GetAllAsync()
        {
            var userRating = await _context.UserRatings.ToListAsync();
            return userRating;
        }

        public async Task<List<UserRating?>> GetByIdAsync(long id)
        {
            var ur = await _context.UserRatings.Where(p => p.ProductId == id).ToListAsync();
            if (ur != null)
            {
                return ur;
            }
            return null;
        }

        public async Task<List<UserRating?>> GetAllRatingOfUserAsync(string id)
        {
            var ur = await _context.UserRatings.Include( s => s.Product).Where(p => p.UsderId == id).ToListAsync();
            if (ur != null)
            {
                return ur;
            }
            return null;
        }

        public async Task<UserRatingResponse> UpdateAsync(long id, UserRatingViewModel ur)
        {
            var _ur = await _context.UserRatings.FindAsync(id);

            if (_ur == null)
                return new UserRatingResponse
                {
                    Rating = ur.Rating,
                    Message = "Bad request",
                    isSuccess = false
                };

            _ur.Rating = ur.Rating;
            _ur.Comment = ur.Comment;
            await _context.SaveChangesAsync();

            return new UserRatingResponse
            {
                Rating = ur.Rating,
                Message = "This User Rating Updated!",
                isSuccess = true
            };
        }

        public async Task<float> getAverageRatingAsync(long id)
        {
            var userRating = _context.UserRatings.Where(ur => ur.ProductId == id);

            var Sum =  _context.UserRatings.Where(ur => ur.ProductId == id).Count();

            int rating = 0;

            foreach(var item in userRating)
            {
                rating += item.Rating;
            }

            float evg = (float)rating / Sum;

            return evg;
        }
    }
}
