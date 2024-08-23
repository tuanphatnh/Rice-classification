using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using Org.BouncyCastle.Bcpg.OpenPgp;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface IActiveSubscriptionService
    {
        Task<List<ActiveSubscription>> GetAllAsync();

        Task<ActiveSubscription?> GetByIdAsync(long id);

        Task<List<ActiveSubscription>> GetByUserIdAsync(string id);

        Task<List<ActiveSubscription>> GetBySubIdAsync(long id);

        Task<ActiveSubscription> GetBySubIdAndUserIdAsync(long SubPacId, string UsrId);

        Task<ActiveSubscriptionResponse> RegisterActiveSubAsync(RegisterViewModel model);

        Task<ActiveSubscriptionResponse> BuyPackageAsync(AddActiveSubscriptionViewModel model, string UsrId);

        Task<ActiveSubscriptionResponse> PostASAsync(long SubPacId, string UsrId);

        Task<ActiveSubscriptionResponse> UpdateAsync(long id, ActiveSubscriptionViewModel model);

        Task<ActiveSubscriptionResponse> DeleteAsync(long id);
    }

    public class ActiveSubscriptionService : IActiveSubscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ActiveSubscriptionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ActiveSubscriptionResponse> BuyPackageAsync(AddActiveSubscriptionViewModel model, string UsrId)
        {
            var _model = new ActiveSubscription
            {
                UsedPost = 0,
                ExpiredDate = DateTime.Now.AddDays(60),
                ActivatedDate = DateTime.Now,
                UserId = UsrId, 
                SpId = model.SpId,
            };
            var user = await _context.Users.FindAsync(UsrId);
            var sp = await _context.SubscriptionPackages.FindAsync(model.SpId);
            if(user != null && sp != null)
                user.UserBalance -= sp.Price;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();


            _context.Add(_model);
            await _context.SaveChangesAsync();

            return new ActiveSubscriptionResponse
            {
                Message = "New Active Subscription Added!",
                isSuccess = true
            };
        }

        // Auto Active Free Subscription Package
        public async Task<ActiveSubscriptionResponse> RegisterActiveSubAsync(RegisterViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            var sp = await _context.SubscriptionPackages.Where(p => p.Name == "Free Subscription Package").FirstOrDefaultAsync();

            if(sp == null)
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Something went wrong with Free Subscription Package !",
                    isSuccess = false
                };
            }    

            var _model = new ActiveSubscription
            {
                UsedPost = 0,
                ExpiredDate = null,
                ActivatedDate = DateTime.Now,
                UserId = user.Id,
                SpId = sp.Id,
            };
            _context.Add(_model);
            await _context.SaveChangesAsync();

            return new ActiveSubscriptionResponse
            {
                Message = "New Active Subscription Added!",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> PostASAsync(long SubPacId, string UsrId)
        {
            //var _model = await _context.ActiveSubscriptions.FirstOrDefaultAsync(p => (p.SpId == SubPacId && p.UserId == UsrId));
            var _model = await _context.ActiveSubscriptions.Where(p => (p.SpId == SubPacId && p.UserId == UsrId)).FirstAsync();


            if (_model == null)
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };
            }
            else
            {

                _model.UsedPost = _model.UsedPost + 1;
                _model.ExpiredDate = _model.ExpiredDate;
                _model.ActivatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return new ActiveSubscriptionResponse
            {
                Message = "This Active Subscription Updated!",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> DeleteAsync(long id)
        {
            var asub = await _context.ActiveSubscriptions.SingleOrDefaultAsync(br => br.Id == id);
            if (asub != null)
            {
                _context.Remove(asub);
                await _context.SaveChangesAsync();

                return new ActiveSubscriptionResponse
                {
                    Message = "This Active Subscription Deleted",
                    isSuccess = true
                };
            }

            return new ActiveSubscriptionResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<ActiveSubscription>> GetAllAsync()
        {
            var asub = await _context.ActiveSubscriptions.ToListAsync();
            return asub;
        }

        public async Task<ActiveSubscription?> GetByIdAsync(long id)
        {
            var asub = await _context.ActiveSubscriptions.SingleOrDefaultAsync(b => b.Id == id);
            if (asub != null)
            {
                return asub;
            }
            return null;
        }

        public async Task<List<ActiveSubscription>> GetByUserIdAsync(string id)
        {
            var asub = await _context.ActiveSubscriptions.Include(p=>p.Sp).Where(b => b.UserId == id).ToListAsync();
            if (asub != null)
            {
                return asub;
            }
            return null;
        }

        public async Task<List<ActiveSubscription>> GetBySubIdAsync(long id)
        {
            var asub = await _context.ActiveSubscriptions.Where(b => b.SpId == id).ToListAsync();
            if (asub != null)
            {
                return asub;
            }
            return null;
        }

        public async Task<ActiveSubscription> GetBySubIdAndUserIdAsync(long SubPacId, string UsrId)
        {
            var asub = await _context.ActiveSubscriptions.Where(p => (p.SpId == SubPacId && p.UserId == UsrId)).FirstAsync(); //.Where(i => i.UserId == UsrId).FirstAsync();
            if (asub != null)
            {
                return asub;
            }
            return null;
        }

        public async Task<ActiveSubscriptionResponse> UpdateAsync(long id, ActiveSubscriptionViewModel model)
        {
            var _model = await _context.ActiveSubscriptions.FindAsync(id);

            if (_model == null)
                return new ActiveSubscriptionResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _model.UsedPost = model.UsedPost;
            _model.ExpiredDate = model.ExpiredDate;
            _model.ActivatedDate = model.ActivatedDate;
            await _context.SaveChangesAsync();

            return new ActiveSubscriptionResponse
            {
                Message = "This Active Subscription Updated!",
                isSuccess = true
            };
        }
    }
}
