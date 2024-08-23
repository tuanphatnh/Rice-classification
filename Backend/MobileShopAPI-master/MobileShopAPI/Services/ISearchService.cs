using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using System.Data.SqlTypes;

namespace MobileShopAPI.Services
{
    public interface ISearchService
    {
        public Task<List<Product>> SearchAllProduct(SearchViewModel model);
        public Task<List<Product>> SearchProductByCategory(SearchViewModel model);
        public Task<List<Product>> SearchProductByBrand(SearchViewModel model);
        public Task<List<Product>> SearchProductByBrandAndCategory(SearchViewModel model);

    }

    public class SearchService:ISearchService
    {
        private readonly ApplicationDbContext _context;
        public SearchService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Product>> SearchAllProduct(SearchViewModel model)
        {
            var productList = await _context.Products.AsNoTracking().Include(p => p.Images)
                .Where(p=>p.isHidden == false && p.Name.Contains(model.KeyWord.Trim()) && (p.Status == 0 || p.Status == 1))
                .ToListAsync();
            return productList;
        }

        public async Task<List<Product>> SearchProductByBrand(SearchViewModel model)
        {
            var productList = await _context.Products.AsNoTracking().Include(p => p.Images).Include(p=>p.Brand)
                .Where(p => 
                    p.isHidden == false && 
                    p.Name.Contains(model.KeyWord.Trim()) && 
                    (p.Status == 0 || p.Status == 1) &&
                    p.Brand.Id == model.BrandId)
                .ToListAsync();
            return productList;
        }

        public async Task<List<Product>> SearchProductByBrandAndCategory(SearchViewModel model)
        {
            var productList = await _context.Products.AsNoTracking().Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p=>p.Brand)
                .Where(p =>
                    p.isHidden == false &&
                    p.Name.Contains(model.KeyWord.Trim()) &&
                    (p.Status == 0 || p.Status == 1) &&
                    p.Category.Id == model.CategoryId &&
                    p.Brand.Id == model.BrandId)
                .ToListAsync();
            return productList;
        }

        public async Task<List<Product>> SearchProductByCategory(SearchViewModel model)
        {
            var productList = await _context.Products.AsNoTracking().Include(p => p.Images).Include(p => p.Category)
                .Where(p =>
                    p.isHidden == false &&
                    p.Name.Contains(model.KeyWord.Trim()) &&
                    (p.Status == 0 || p.Status == 1) &&
                    p.Category.Id == model.CategoryId)
                .ToListAsync();
            return productList;
        }
    }
}
