using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface IColorService
    {
        Task<List<Color>> GetAllAsync();

        Task<Color?> GetByIdAsync(long id);

        Task<ColorResponse> AddAsync(ColorViewModel color);

        Task<ColorResponse> UpdateAsync(long id,ColorViewModel color);

        Task<ColorResponse> DeleteAsync(long id);
    }

    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ColorResponse> AddAsync(ColorViewModel color)
        {
            var _color = new Color
            {
                ColorName = color.ColorName,
                HexValue = color.HexValue
            };

            _context.Add(_color);
            await _context.SaveChangesAsync();


            return new ColorResponse
            {
                Message = "New Color Added!",
                isSuccess = true
            };
        }

        public async Task<ColorResponse> DeleteAsync(long id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Remove(color);
                await _context.SaveChangesAsync();

                return new ColorResponse
                {
                    Message = "This Color Deleted",
                    isSuccess = true
                };
            }

            return new ColorResponse
            {
                Message = "DeleteAsync Fail",
                isSuccess = false
            };
        }

        public async Task<List<Color>> GetAllAsync()
        {
            var colors = await _context.Colors.ToListAsync();
            return colors;
        }

        public async Task<Color?> GetByIdAsync(long id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                return color;
            }
            return null;
        }

        public async Task<ColorResponse> UpdateAsync(long id,ColorViewModel color)
        {
            var _color = await _context.Colors.FindAsync(id);
            if(_color == null)
                return new ColorResponse
                {
                    Message = "Not found!",
                    isSuccess = false
                };
            _color.ColorName = color.ColorName;
            _color.HexValue = color.HexValue;
            _color.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ColorResponse
            {
                Message = "This Color is Updated!",
                isSuccess = true
            };
        }
    }
}
