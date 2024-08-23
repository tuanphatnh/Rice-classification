    using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface ICoinActionService
    {
        Task<List<CoinAction>> GetAllAsync();

        Task<CoinAction?> GetByIdAsync(long id);

        Task<CoinActionResponse> AddAsync(CoinActionViewModel ca);

        Task<CoinActionResponse> UpdateAsync(long id, CoinActionViewModel ca);

        Task<CoinActionResponse> DeleteAsync(long id);

        public class CoinActionService : ICoinActionService
        {
            private readonly ApplicationDbContext _context;

            public CoinActionService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CoinActionResponse> AddAsync(CoinActionViewModel ca)
            {
                var _ca = new CoinAction
                {
                    ActionName = ca.ActionName,
                    Description = ca.Description,
                    CaCoinAmount = ca.CaCoinAmount,
                    Status = 1,
                    CreatedDate = null,
                    UpdatedDate = null
                };
                _context.Add(_ca);
                await _context.SaveChangesAsync();

                return new CoinActionResponse
                {
                    Message = "New Coin_Action Added!",
                    isSuccess = true
                };
            }

            public async Task<CoinActionResponse> DeleteAsync(long id)
            {
                var ca = await _context.CoinActions.SingleOrDefaultAsync(br => br.Id == id);
                if (ca != null)
                {
                    _context.Remove(ca);
                    await _context.SaveChangesAsync();

                    return new CoinActionResponse
                    {
                        Message = "This Coin_Action Deleted",
                        isSuccess = true
                    };
                }

                return new CoinActionResponse
                {
                    Message = "DeleteAsync Fail !!!",
                    isSuccess = false
                };
            }

            public async Task<List<CoinAction>> GetAllAsync()
            {
                var cas = await _context.CoinActions.ToListAsync();
                return cas;
            }

            public async Task<CoinAction?> GetByIdAsync(long id)
            {
                var ca = await _context.CoinActions.SingleOrDefaultAsync(b => b.Id == id);
                if (ca != null)
                {
                    return ca;
                }
                return null;
            }

            public async Task<CoinActionResponse> UpdateAsync(long id, CoinActionViewModel ca)
            {
                var _ca = await _context.CoinActions.FindAsync(id);

                if (_ca == null)
                    return new CoinActionResponse
                    {
                        Message = "Bad request",
                        isSuccess = false
                    };

                _ca.ActionName = ca.ActionName;
                _ca.Description = ca.Description;
                _ca.CaCoinAmount = ca.CaCoinAmount;
                _ca.Status = ca.Status; 
                _ca.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();

                return new CoinActionResponse
                {
                    Message = "This Coin_Action Updated!",
                    isSuccess = true
                };
            }
        }
    }
}
