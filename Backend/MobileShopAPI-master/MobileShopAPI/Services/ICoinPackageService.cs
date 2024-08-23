using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface ICoinPackageService
    {
        Task<List<CoinPackage>> GetAllAsync();

        Task<CoinPackage?> GetByIdAsync(string id);

        Task<CoinPackageResponse> AddAsync(CoinPackageViewModel cp);

        Task<CoinPackageResponse> UpdateAsync(string id, CoinPackageViewModel cp);

        Task<CoinPackageResponse> DeleteAsync(string id);
    }

    public class CoinPackageService : ICoinPackageService
    {
        private readonly ApplicationDbContext _context;

        public CoinPackageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CoinPackageResponse> AddAsync(CoinPackageViewModel cp)
        {
            var _cp = new CoinPackage
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                PackageName = cp.PackageName,
                PackageValue = cp.PackageValue,
                ValueUnit = cp.ValueUnit,
                CoinAmount = cp.CoinAmount,
                Description = cp.Description,
                CreatedDate = null,
                UpdatedDate = null
            };
            _context.Add(_cp);
            await _context.SaveChangesAsync();

            return new CoinPackageResponse
            {
                Message = "New Coin Package Added!",
                isSuccess = true
            };
        }

        public async Task<CoinPackageResponse> DeleteAsync(string id)
        {
            var cp = await _context.CoinPackages.SingleOrDefaultAsync(br => br.Id == id);
            if (cp != null)
            {
                _context.Remove(cp);
                await _context.SaveChangesAsync();

                return new CoinPackageResponse
                {
                    Message = "This Coin Package Deleted",
                    isSuccess = true
                };
            }

            return new CoinPackageResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<CoinPackage>> GetAllAsync()
        {
            var cps = await _context.CoinPackages.ToListAsync();
            return cps;
        }

        public async Task<CoinPackage?> GetByIdAsync(string id)
        {
            var cp = await _context.CoinPackages.SingleOrDefaultAsync(b => b.Id == id);
            if (cp != null)
            {
                return cp;
            }
            return null;
        }

        public async Task<CoinPackageResponse> UpdateAsync(string id, CoinPackageViewModel cp)
        {
            var _cp = await _context.CoinPackages.FindAsync(id);

            if (_cp == null)
                return new CoinPackageResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _cp.PackageName = cp.PackageName;
            _cp.PackageValue = cp.PackageValue;
            _cp.ValueUnit = cp.ValueUnit;
            _cp.CoinAmount = cp.CoinAmount;
            _cp.Description = cp.Description;
            _cp.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new CoinPackageResponse
            {
                Message = "This Coin Package Updated!",
                isSuccess = true
            };
        }
    }
}
