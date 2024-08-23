using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IProductOrderService
    {
        Task AddAsync(string orderId,long productId, ProductOrderViewModel productOrder);

        //Task UpdateAsync(ImageViewModel image);

        //Task DeleteAsync(long id);
    }
    public class ProductOrderService : IProductOrderService
    {
        private readonly ApplicationDbContext _context;

        public ProductOrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(string orderId, long productId, ProductOrderViewModel productOrder)
        {
            var _productOrder = new ProductOrder
            {
                
                OrderId = orderId,
                ProductId = productId,
                quantity = productOrder.Quantity
            };
            _context.ProductOrders.Add(_productOrder);
            await _context.SaveChangesAsync();
        }
    }
}
