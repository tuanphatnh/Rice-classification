using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using System.Numerics;

namespace MobileShopAPI.Services
{
    public interface IImageService
    {
        Task AddAsync(long productId,ImageViewModel image);

        Task UpdateAsync(long productId,ImageViewModel image);

        Task DeleteAsync(long productId, long id);

        Task CheckCover(long productId);
    }

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(long productId,ImageViewModel image)
        {
            var _image = new Image
            {
                Url = image.Url,
                IsCover = image.IsCover,
                IsVideo = image.IsVideo,
                ProductId = productId
            };
            _context.Images.Add(_image);
            await _context.SaveChangesAsync();
        }

        public async Task CheckCover(long productId)
        {
            var product = await _context.Products
                .Include(p=>p.Images)
                .Where(p=>p.Id == productId)
                .SingleOrDefaultAsync();
            if (product == null) return;
            int NumberOfCover = product.Images.Where(p => p.IsCover == true).Count();
            if (NumberOfCover == 1) return;
            if (NumberOfCover >= 2 || NumberOfCover ==0)
            {
                await ResetProductCover(product);
            }
        }

        public async Task ResetProductCover(Product product)
        {
            bool hasCover = false;
            foreach (var image in product.Images)
            {
                if(!hasCover)
                {
                    hasCover = true;
                    image.IsCover = true;
                    _context.Images.Update(image);
                }
                else
                {
                    image.IsCover = false;
                    _context.Images.Update(image);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long productId,long id)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .Where(p => p.Id == productId)
                .SingleOrDefaultAsync();
            if (product == null) return;
            if (product.Images.Count <= 2) return;
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(long productId, ImageViewModel image)
        {
            var _image = await _context.Images.FindAsync(image.Id);
            if (_image == null)
                return;
            var cover = await _context.Images.Where(p => p.IsCover == true & p.ProductId == productId).SingleOrDefaultAsync();
            if (cover != null)
            {
                cover.IsCover = false;
                _context.Images.Update(cover);
                await _context.SaveChangesAsync();
            }
            _image.IsCover = image.IsCover;
            _image.UpdatedDate = DateTime.Now;
            _context.Images.Update(_image);
            await _context.SaveChangesAsync();
        }


    }
}
