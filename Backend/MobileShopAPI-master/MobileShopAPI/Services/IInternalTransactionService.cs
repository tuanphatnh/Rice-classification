using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface IInternalTransactionService
    {
        Task<List<InternalTransaction>> GetAllAsync();

        Task<List<InternalTransaction?>> GetByUserIdAsync(string id);

        Task<List<InternalTransaction>> GetBySubscriptionIdAsync(long id);

        Task<List<InternalTransaction>> GetByCoinActionIdAsync(long id);

        Task<InternalTransactionResponse> PushUpAsync(string UsrId);

        Task<InternalTransactionResponse> BuyPackageAsync(string UsrId, long? SubPacId);

        Task<InternalTransactionResponse> PostAsync(string UsrId, long? SubPacId);
    }

    public class InternalTransactionService : IInternalTransactionService
    {
        private readonly ApplicationDbContext _context;

        public InternalTransactionService(ApplicationDbContext context)
        {
            _context = context;
        }
        // Đẩy tin
        public async Task<InternalTransactionResponse> PushUpAsync(string UsrId)
        {
            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 1).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = 1,
                SpId = null,
                ItAmount = null,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Push Up) Added!",
                isSuccess = true
            };
        }

        // Mua gói tin đăng
        public async Task<InternalTransactionResponse> BuyPackageAsync(string UsrId, long? SubPacId)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacId).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 2).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = 2,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Buy Package) Added!",
                isSuccess = true
            };
        }

        // Đăng tin
        public async Task<InternalTransactionResponse> PostAsync(string UsrId, long? SubPacId)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacId).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 3).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = 3,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Post) Added!",
                isSuccess = true
            };
        }

        public async Task<List<InternalTransaction>> GetAllAsync()
        {
            var its = await _context.InternalTransactions.ToListAsync();
            return its;
        }

        public async Task<List<InternalTransaction>> GetBySubscriptionIdAsync(long id)
        {
            var its = await _context.InternalTransactions.Where(sp => sp.SpId == id).ToListAsync();
            return its;
        }

        public async Task<List<InternalTransaction?>> GetByUserIdAsync(string id)
        {
            var its = await _context.InternalTransactions.Where(sp => sp.UserId == id).ToListAsync();
            return its;
        }

        public async Task<List<InternalTransaction?>> GetByCoinActionIdAsync(long id)
        {
            var its = await _context.InternalTransactions.Where(sp => sp.CoinActionId == id).ToListAsync();
            return its;
        }
    }
}
