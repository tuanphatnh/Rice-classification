using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();

        Task<Brand?> GetByIdAsync(long id);

        Task<BrandResponse> AddAsync(BrandViewModel brand);

        Task<BrandResponse> UpdateAsync(long id,BrandViewModel brand);

        Task<BrandResponse> DeleteAsync(long id);
    }

    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BrandResponse> AddAsync(BrandViewModel brand)
        {
            var _brand = new Brand
            {
                Name = brand.Name,
                Description = brand.Description,
                ImageUrl = brand.ImageUrl,
                CreatedDate = null
            };
            _context.Add(_brand);
            await _context.SaveChangesAsync();

            return new BrandResponse
            {
                Message = "New Brand Added!",
                isSuccess = true
            };

        }

        public async Task<BrandResponse> DeleteAsync(long id)
        {
            var brand = await _context.Brands.SingleOrDefaultAsync(br => br.Id == id);
            if(brand != null)
            {
                _context.Remove(brand);
                await _context.SaveChangesAsync();

                return new BrandResponse
                {
                    Message = "This Brand Deleted",
                    isSuccess = true
                };
            }

            return new BrandResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            var brands = await _context.Brands.ToListAsync();
            return brands;
        }

        public async Task<Brand?> GetByIdAsync(long id)
        {
            var brand = await _context.Brands.Include(p=>p.Products).SingleOrDefaultAsync(b => b.Id == id);
            if(brand != null)
            {
                return brand;
            }
            return null;
        }

        public async Task<BrandResponse> UpdateAsync(long id,BrandViewModel brand)
        {
            var _brand = await _context.Brands.FindAsync(id);

            if(_brand == null)
                return new BrandResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _brand.Name = brand.Name;
            _brand.Description = brand.Description;
            _brand.ImageUrl = brand.ImageUrl;
            _brand.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new BrandResponse
            {
                Message = "This Brand Updated!",
                isSuccess = true
            };
        }
    }
}
