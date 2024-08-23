using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IShippingAddressService
    {
        Task<List<ShippingAddress>?> GetAllUserAddressAsync(string userId);

        Task<ShippingAddress?> GetByIdAsync(long id);

        Task<ShippingAddressResponse> CreateAddressAsync(ShippingAddressViewModel model);

        Task<ShippingAddressResponse> UpdateAddressAsync(long id, ShippingAddressViewModel model);

        Task<ShippingAddressResponse> DeleteAddressAsync(long id);
    }


    public class ShippingAddressService : IShippingAddressService
    {
        private readonly ApplicationDbContext _context;

        public ShippingAddressService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ShippingAddressResponse> CreateAddressAsync(ShippingAddressViewModel model)
        {
            var address = new ShippingAddress
            {
                AddressName = model.AddressName,
                PhoneNumber = model.PhoneNumber,
                IsDefault = model.IsDefault,
                WardId = model.WardId,
                UserId = model.UserId,
                AddressDetail = model.AddressDetail

            };
            _context.ShippingAddresses.Add(address);
            await _context.SaveChangesAsync();

            return new ShippingAddressResponse
            {
                Message = "Address has been created successfully",
                isSuccess = true
            };
        }

        public async Task<ShippingAddressResponse> DeleteAddressAsync(long addressId)
        {
            var address = await _context.ShippingAddresses.SingleOrDefaultAsync(br => br.Id == addressId);
            if (address != null)
            {
                _context.Remove(address);
                await _context.SaveChangesAsync();

                return new ShippingAddressResponse
                {
                    Message = "This Address Deleted",
                    isSuccess = true
                };
            }

            return new ShippingAddressResponse
            {
                Message = "Delete Fail",
                isSuccess = false
            };

        }

        public async Task<ShippingAddressResponse> UpdateAddressAsync(long addId, ShippingAddressViewModel model)
        {
            var address = await _context.ShippingAddresses.FindAsync(addId);
            if (address == null)
            {
                return new ShippingAddressResponse
                {
                    Message = "Address not found",
                    isSuccess = false
                };
            }

            address.UserId = model.UserId;
            address.AddressDetail = model.AddressDetail;
            address.AddressName = model.AddressName;
            address.PhoneNumber = model.PhoneNumber;
            address.IsDefault = model.IsDefault;
            address.WardId = model.WardId;
            address.UserId = model.UserId;
            address.UpdatedDate = DateTime.Now;

            _context.ShippingAddresses.Update(address);
            await _context.SaveChangesAsync();

            
            return new ShippingAddressResponse
            {
                Message = "Address has been updated successfully",
                isSuccess = true
            };
        }

        public async Task<List<ShippingAddress>?> GetAllUserAddressAsync(string userId)
        {
            var addressList = await _context.ShippingAddresses.AsNoTracking()
                .Where(p=>p.UserId == userId)
                .ToListAsync();
            if (addressList == null) return null;
            return addressList;
        }
        public async Task<ShippingAddress?> GetByIdAsync(long id)
        {
            var address = await _context.ShippingAddresses.SingleOrDefaultAsync(x => x.Id == id);
            if (address != null)
            {
                return address;
            }
            return null;
        }
    }
}
