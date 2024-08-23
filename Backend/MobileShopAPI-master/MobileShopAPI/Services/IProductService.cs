using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using Swashbuckle.SwaggerUi;
using System.Buffers.Text;
using System.Text;

namespace MobileShopAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductAsync();

        Task<List<Product>> GetAllNoneHiddenProductAsync();

        Task<ProductResponse> ApproveProduct(long productId);

        Task<ProductDetailViewModel?> GetProductDetailAsync(long productId);

        Task<ProductDetailViewModel?> GetNoneHiddenProductDetailAsync(long productId);

        Task<ProductResponse> CreateProductAsync(string userId, ProductViewModel model);

        Task<ProductResponse> EditProductAsync(long productId,ProductViewModel model);

        Task<ProductResponse> DeleteProductAsync(long productId);

        Task<ProductResponse> AddMarkedProductAsync(string userId, long productId);

        Task<ProductResponse> RemoveMarkedProductAsync(string userId, long productId);

        Task<List<Product>> ListUserMarkedProduct(string userId);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public ProductService(ApplicationDbContext context,IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<ProductResponse> ApproveProduct(long productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = "Product not found",
                    isSuccess = false
                };
            }
            product.isHidden = false;
            if(product.Stock <= 0) 
            { 
                product.Stock = 0;
                product.Status = 1;
            }
            else
            {
                product.Status = 0;
            }
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Message = "Product has been approved",
                isSuccess = true
            };
        }

        public async Task<ProductResponse> CreateProductAsync(string userId, ProductViewModel model)
        {
            bool check = false;//Check of censored words
            if (model == null)
                return new ProductResponse
                {
                    Message = "Model is null",
                    isSuccess = false
                };
            //censored words
            if (model.Description != null)
            {
                check = CensoredWord.isCensoredWord(model.Description);
                if (check)
                {
                    return new ProductResponse
                    {
                        Message = "censored word: " + CensoredWord.result,
                        isSuccess = false
                    };
                }
            }
            //End - censored words
            if (model.Images == null)
                return new ProductResponse
                {
                    Message = "Images list is null",
                    isSuccess = false
                };
            if (model.Images.Count < 2)
            {
                return new ProductResponse
                {
                    Message = "Product need at least 2 image",
                    isSuccess = false
                };
            }

            bool hasCover = false;
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Stock = model.Stock,
                Price = model.Price,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                UserId = userId,
                SizeId = model.SizeId,
                ColorId = model.ColorId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (model.Images != null)
                foreach (var item in model.Images)
                {
                    var image = new ImageViewModel();
                    if (!hasCover)
                    {

                        image.IsCover = true;
                        hasCover = true;
                    }
                    else
                    {
                        image.IsCover = false;
                    }
                    image.Url = item.Url;
                    image.IsVideo = item.IsVideo;
                    await _imageService.AddAsync(product.Id, image);
                }

            return new ProductResponse
            {
                Message = "Product has been created successfully",
                isSuccess = true
            };
        }

        public async Task<ProductResponse> DeleteProductAsync(long productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId && p.Status != 3)
                .Include(p => p.ProductOrders)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = "Product not found!",
                    isSuccess = false
                };
            }
            if(product.ProductOrders.Any())
            {
                product.Status = 3;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new ProductResponse
                {
                    Message = "Product has been soft deleted!",
                    isSuccess = true
                };
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Message = "Product has been deleted!",
                isSuccess = true
            };

        }

        public async Task<ProductResponse> EditProductAsync(long productId,ProductViewModel model)
        {
            bool check = false;//Check of censored words
            var product = await _context.Products
                .Where(p => p.Id == productId && p.Status != 3)
                .SingleOrDefaultAsync();
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = "Product not found",
                    isSuccess = false
                };
            }
            if (model.Description != null)
            {
                check = CensoredWord.isCensoredWord(model.Description);
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.Stock = model.Stock;
            product.Price = model.Price;
            if (check)
                product.Status = 2;
            else if (product.Stock <= 0)
                product.Status = 1;
            else if (product.Status == 2)
                product.Status = 2;
            else
                product.Status = 0;
            product.CategoryId = model.CategoryId;
            product.BrandId = model.BrandId;
            product.SizeId = model.SizeId;
            product.ColorId = model.ColorId;
            product.isHidden = model.isHidden;
            product.UpdatedDate = DateTime.Now;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            if (model.Images != null)
                foreach (var item in model.Images)
                {
                    if(item.IsDeleted)
                    {
                        await _imageService.DeleteAsync(productId, item.Id);
                        continue;
                    }
                    if(item.IsNewlyAdded)
                    {
                        await _imageService.AddAsync(productId, item);
                        continue;
                    }
                    await _imageService.UpdateAsync(productId,item);
                }
            await _imageService.CheckCover(productId);
            if (check)
                return new ProductResponse
                {
                    Message = "Product has been hidden, because censored word: " + CensoredWord.result,
                    isSuccess = true
                };
            else
                return new ProductResponse
                {
                    Message = "Product has been updated successfully",
                    isSuccess = true
                };
        }

        public async Task<List<Product>> GetAllNoneHiddenProductAsync()
        {
            var productList = await _context.Products.AsNoTracking()
                .Where(p=>p.isHidden == false && (p.Status == 0 || p.Status == 1))
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();

            return productList;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            var productList = await _context.Products.AsNoTracking()
                .Include(p => p.Images)
                .Include(p=>p.Brand)
                .Include(p => p.Category)
                .ToListAsync();

            return productList;
        }

        public async Task<ProductDetailViewModel?> GetNoneHiddenProductDetailAsync(long productId)
        {
            var product = await _context.Products.AsNoTracking().Where(p => p.Id == productId)
                .Where(p => p.isHidden == false && (p.Status == 0 || p.Status == 1))
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p=>p.UserRatings)
                .SingleOrDefaultAsync();
            if (product == null) return null;

            var user = await _context.Users.Where(u => u.Id == product.UserId).SingleOrDefaultAsync();
            if (user == null) return null;

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
                UpdatedDate = user.UpdatedDate
            };

            var returnValue = new ProductDetailViewModel
            {
                Product = product,
                User = userView
            };


            return returnValue;
        }

        public async Task<ProductDetailViewModel?> GetProductDetailAsync(long productId)
        {
            var product = await _context.Products.AsNoTracking().Where(p=>p.Id == productId)
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p=>p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p => p.UserRatings)
                .SingleOrDefaultAsync();
            if (product == null) return null;

            var user = await _context.Users.Where(u=>u.Id == product.UserId).SingleOrDefaultAsync();
            if(user == null) return null;

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
                UpdatedDate = user.UpdatedDate
            };

            var returnValue = new ProductDetailViewModel
            {
                Product = product,
                User = userView
            };


            return returnValue;
        }

        public async Task<ProductResponse> AddMarkedProductAsync(string userId,long productId)
        {
            var marked = await _context.MarkedProducts.Where(p => p.ProductId == productId && p.UserId == userId).FirstOrDefaultAsync();
            if (marked != null) return new ProductResponse
            {
                Message = "Product is already in the marked product list",
                isSuccess = false,
            };
            var mark = new MarkedProduct
            {
                ProductId = productId,
                UserId = userId
            };
            _context.MarkedProducts.Add(mark);
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Message = "Product has been added to the marked list",
                isSuccess = true,
            };
        }

        public async Task<ProductResponse> RemoveMarkedProductAsync(string userId, long productId)
        {
            var marked = await _context.MarkedProducts.Where(p => p.ProductId == productId && p.UserId == userId).FirstOrDefaultAsync();
            if (marked == null) return new ProductResponse
            {
                Message = "Product is not in the marked product list of user",
                isSuccess = false,
            };

            _context.MarkedProducts.Remove(marked);

            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Message = "Product has been removed",
                isSuccess = true,
            };

        }

        public async Task<List<Product>> ListUserMarkedProduct(string userId)
        {
            var listMarkedProduct = await _context.Products.AsNoTracking()
                .Include(p => p.MarkedProducts)
                .Where(p => p.MarkedProducts.Any(p => p.UserId == userId))
                .ToListAsync();

            return listMarkedProduct;
        }
    }
}
