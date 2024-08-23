using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel;

namespace MobileShopAPI.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrderAsync();

        Task<Order?> GetOrderDetailAsync(string id);

     
        Task<OrderResponse> CreateOrderAsync(OrderViewModel model);

        Task<OrderResponse> UpdateOrderAsync(string id, OrderUpdateViewModel model);

        Task<OrderResponse> DeleteOrderAsync(string id);
        Task<List<Order>> GetListOrderByUser(string userId);
        //Task<List<Order>> GetListBuyerByUser(string userId);

        

    }
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IProductOrderService _productOrderService;
        public OrderService(ApplicationDbContext context, IProductOrderService productOrderService )
        {
            _context = context;
            _productOrderService = productOrderService;
           
        }
        public async Task<OrderResponse> CreateOrderAsync(OrderViewModel model)
        {
            
            var order = new Order
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                Total = model.Total,
                
                UserId = model.UserId,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserFullName = model.UserFullName,
                Type = model.Type
                
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            if (model.ProductOrder != null)
            {
                foreach (var item in model.ProductOrder)
                {
                    var product = new ProductOrderViewModel();
                    {
                        product.Quantity = item.Quantity;
                        product.ProductId = item.ProductId;
                        product.OrderId = item.OrderId;
                    };
                    await _productOrderService.AddAsync(order.Id, product.ProductId, product);
                }

            }

            return new OrderResponse
            {
                Message = "Order has been created successfully",
                isSuccess = true
            };
        }
        public async Task<List<Order>> GetAllOrderAsync()
        {
            var orderList = await _context.Orders.ToListAsync();
            return orderList;
        }
        public async Task<Order?> GetOrderDetailAsync(string id)
        {
            var order = await _context.Orders.Include(p=> p.ProductOrders).SingleOrDefaultAsync(x=>x.Id == id);
            if (order != null)
                return order;
            return null;
            
        }

        public async Task<OrderResponse> UpdateOrderAsync(string id,OrderUpdateViewModel model)
        {
            var order = await _context.Orders.FindAsync(id);
            
            if (order == null)
            {
                return new OrderResponse
                {
                    Message = "Order not found",
                    isSuccess = false
                };
            }

            order.Status = model.Status;
            order.Address = model.Address;
            order.PhoneNumber = model.PhoneNumber;
            order.Email = model.Email;
            order.UserFullName = model.UserFullName;
            order.Type = model.Type;
            order.UpdateDate= DateTime.Now;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            
            return new OrderResponse
            {
                Message = "Order has been updated successfully",
                isSuccess = true
            };
        }
        public async Task<OrderResponse> DeleteOrderAsync(string id)
        {
            var order = await _context.Orders.Where(p => p.Id == id  && p.Status != 3 )
                .Include(p => p.ProductOrders)
                .Include(p=>p.Transactions)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return new OrderResponse
                {
                    Message = "Order not found!",
                    isSuccess = false
                };
            }
            if(order.Transactions != null)
            {
                return new OrderResponse
                {
                    Message = "Order has been paid",
                    isSuccess = false
                };
            }
            if (order.ProductOrders.Any())
            {
                order.Status = 3;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return new OrderResponse
                {
                    Message = "Order has been soft deleted!",
                    isSuccess = true
                };
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return new OrderResponse
            {
                Message = "Order has been deleted!",
                isSuccess = true
            };

        }
        public async Task<List<Order>>GetListOrderByUser(string userid)
        {
            var orderList = await _context.Orders.ToListAsync();
            foreach (var id in orderList)
            {
                if(id.UserId.Equals(userid))
                {
                    return orderList;
                }
            }
            return null;
        }

        //public async Task<List<ProductOrder>> GetListBuyerByUser(string id)
        //{
        //    var ProductListbyuser = await _context.Products.Where(x=>x.UserId== id).ToListAsync();
            
        //    return null;
        //}

        //public async Task<List<Order>> GetListBuyerByUser(string userid)
        //{
        //    var query = from a in _context.Orders
        //                join cif in _context.ProductOrders on a.Id equals cif.OrderId into result1
        //                from commandInFunction in result1.DefaultIfEmpty()
        //                join f in _context.Products on commandInFunction.ProductId equals f.Id into result2
        //                from function in result2.DefaultIfEmpty()
        //                select new
        //                {
        //                    a.Id,
        //                    a.UserId,
        //                    a.PhoneNumber,
        //                    a.UserFullName,
        //                    a.Address,
        //                    a.Email,
        //                    a.Status,
        //                    a.Total,
        //                    a.Type,
        //                    commandInFunction.ProductId
        //                };

        //    query = query.Where(x => x.UserId == userid);

        //    var data = await query.Select(x => new OrderViewModel()
        //    {
        //        Id = x.Id,
        //        UserId = x.UserId,
        //        PhoneNumber = x.PhoneNumber,
        //        UserFullName = x.UserFullName,
        //        Address = x.Address,
        //        Email = x.Email,
        //        Status = x.Status,
        //        Total = x.Total,
        //        Type = x.Type,
        //    }).ToListAsync();

        //    return data;
        //}
        

    }
}
