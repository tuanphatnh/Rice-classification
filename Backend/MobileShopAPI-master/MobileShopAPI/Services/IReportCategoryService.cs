using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IReportCategoryService
    {
        Task<List<ReportCategory>> GetAllAsync();

        Task<ReportCategory?> GetByIdAsync(long id);

        Task<ReportCategoryResponse> AddAsync(ReportCategoryViewModel reportCategory);

        Task<ReportCategoryResponse> UpdateAsync(long id, ReportCategoryViewModel reportCategory);

        Task<ReportCategoryResponse> DeleteAsync(long id);
    }

    public class ReportCategoryService : IReportCategoryService
    {
        private readonly ApplicationDbContext _context;

        public ReportCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReportCategoryResponse> AddAsync(ReportCategoryViewModel reportCategory)
        {
            var _reportCategory = new ReportCategory
            {
                Name = reportCategory.Name,
                Description = reportCategory.Description,
                CreatedDate = null
            };
            _context.Add(_reportCategory);
            await _context.SaveChangesAsync();

            return new ReportCategoryResponse
            {
                Message = "New report category Added!",
                isSuccess = true
            };

        }

        public async Task<ReportCategoryResponse> DeleteAsync(long id)
        {
            var reportCategory = await _context.ReportCategories.SingleOrDefaultAsync(rc => rc.Id == id);
            if (reportCategory != null)
            {
                _context.Remove(reportCategory);
                await _context.SaveChangesAsync();

                return new ReportCategoryResponse
                {
                    Message = "This report category Deleted",
                    isSuccess = true
                };
            }

            return new ReportCategoryResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<ReportCategory>> GetAllAsync()
        {
            var reportCategories = await _context.ReportCategories.ToListAsync();
            return reportCategories;
        }

        public async Task<ReportCategory?> GetByIdAsync(long id)
        {
            var reportCategory = await _context
                .ReportCategories
                .Include(rc => rc.Reports)
                .SingleOrDefaultAsync(rc => rc.Id == id);
            if (reportCategory != null)
            {
                return reportCategory;
            }
            return null;
        }

        public async Task<ReportCategoryResponse> UpdateAsync(long id, ReportCategoryViewModel reportCategory)
        {
            var _reportCategory = await _context.ReportCategories.FindAsync(id);

            if (_reportCategory == null)
                return new ReportCategoryResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _reportCategory.Name = reportCategory.Name;
            _reportCategory.Description = reportCategory.Description;
            _reportCategory.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ReportCategoryResponse
            {
                Message = "This report category Updated!",
                isSuccess = true
            };
        }
    }
}
