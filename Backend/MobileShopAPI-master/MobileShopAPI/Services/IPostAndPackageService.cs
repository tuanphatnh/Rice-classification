using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IPostAndPackageService
    {
        Task<ProductResponse> CreateProductAsync(ProductSpIdViewModel model, string UsrId);

        Task<ActiveSubscriptionResponse> AS_BuyPackageAsync(AddActiveSubscriptionViewModel model, string UsrId);

        Task<ActiveSubscriptionResponse> AS_PostAsync(long SubPacId, string UsrId);

        Task<InternalTransactionResponse> IT_BuyPackageAsync(string UsrId, long? SubPacId);

        Task<InternalTransactionResponse> IT_PostAsync(string UsrId, long? SubPacId);

        Task<InternalTransactionResponse> IT_PushUpAsync(string UsrId, long ProductId);

        Task<ProductResponse> SetPriorities(string UsrId, long ProductId);

        Task<List<Product>> SortList(List<Product> pro);

        Task<ProductResponse> HideProduct(long id);

    }

    public class PostAndPackageService : IPostAndPackageService
    {
        private readonly ApplicationDbContext _context;

        private readonly IImageService _imageService;

        private readonly UserManager<ApplicationUser> _userManager;


        public PostAndPackageService(ApplicationDbContext context, IImageService imageService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _userManager = userManager;
        }

         public async Task<ProductResponse> CreateProductAsync(ProductSpIdViewModel model, string UsrId)
        {
            if (model == null)
                return new ProductResponse
                {
                    Message = "Model is null",
                    isSuccess = false
                };
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
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Stock = model.Stock,
                Price = model.Price,
                Status = model.Status,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                UserId = UsrId,
                SizeId = model.SizeId,
                Priorities = 2,
                ColorId = model.ColorId,
                ExpiredDate = DateTime.Now.AddDays(60)
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            bool hasCover = false;
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
                    await _imageService.AddAsync(product.Id, image);
                }

            return new ProductResponse
            {
                Message = "Product has been created successfully",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> AS_BuyPackageAsync(AddActiveSubscriptionViewModel model, string UsrId)
        {
            var user = await _userManager.FindByIdAsync(UsrId);
            var SP = await _context.SubscriptionPackages.Where(p => p.Id == model.SpId).FirstOrDefaultAsync();

            if(user.UserBalance == null || user.UserBalance < SP.Price)
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Not Enougn Coin !",
                    isSuccess = false
                };
            }  
            
            var _model = new ActiveSubscription
            {
                UsedPost = 0,
                ExpiredDate = DateTime.Now.AddDays(60),
                ActivatedDate = DateTime.Now,
                UserId = UsrId,
                SpId = model.SpId,
            };
            _context.Add(_model);
            await _context.SaveChangesAsync();

            return new ActiveSubscriptionResponse
            {
                Message = "New Active Subscription Added!",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> AS_PostAsync(long SubPacId, string UsrId)
        {
            var sp = await _context.SubscriptionPackages.FirstOrDefaultAsync(p => p.Id == SubPacId);
            var _model = await _context.ActiveSubscriptions.Where(p => (p.SpId == SubPacId && p.UserId == UsrId)).FirstOrDefaultAsync();

            var user = await _userManager.FindByIdAsync(UsrId);
            var SP = await _context.SubscriptionPackages.Where(p => p.Id == SubPacId).FirstOrDefaultAsync();

            if (user.UserBalance == null || user.UserBalance < SP.Price)
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Not Enougn Coin !",
                    isSuccess = false
                };
            }

            if (sp != null && _model != null)
            {
                // Free Subcription Package
                if(sp.Name == "Free Subscription Package") 
                {
                    if (_model.UsedPost == sp.PostAmout)
                    {
                        _model.UsedPost = 0;
                        _model.ExpiredDate = _model.ExpiredDate;
                        _model.ActivatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _model.UsedPost = _model.UsedPost + 1;
                        _model.ExpiredDate = _model.ExpiredDate;
                        _model.ActivatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }    
                }
                else // Paid Subcription Package
                {
                    if (_model.UsedPost + 1 == sp.PostAmout)
                    {
                        _context.Remove(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _model.UsedPost = _model.UsedPost + 1;
                        _model.ExpiredDate = _model.ExpiredDate;
                        _model.ActivatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                }          
            }
            else
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };
            }

            //if (_model == null)
            //{
            //    return new ActiveSubscriptionResponse
            //    {
            //        Message = "Bad request",
            //        isSuccess = false
            //    };
            //}
            //else
            //{

            //    _model.UsedPost = _model.UsedPost + 1;
            //    _model.ExpiredDate = _model.ExpiredDate;
            //    _model.ActivatedDate = DateTime.Now;
            //    await _context.SaveChangesAsync();
            //}

            return new ActiveSubscriptionResponse
            {
                Message = "this active subscription updated!",
                isSuccess = true
            };
        }

        // Mua gói tin đăng
        public async Task<InternalTransactionResponse> IT_BuyPackageAsync(string UsrId, long? SubPacId)
        {
            var SP = _context.SubscriptionPackages.Where(sp => sp.Id == SubPacId).FirstOrDefault();

            if(SP == null)
            {
                return new InternalTransactionResponse
                {
                    Message = "This Subscription Package Does Not Exits !",
                    isSuccess = false
                };
            }    

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.ActionName == "Mua gói tin").FirstOrDefault();

            if (CA == null)
            {
                return new InternalTransactionResponse
                {
                    Message = "This Coin Action Does Not Exits !",
                    isSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(UsrId);

            if(user.UserBalance == null || user.UserBalance < SP.Price)
            {
                return new InternalTransactionResponse
                {
                    Message = "Not Enough Coin !",
                    isSuccess = false
                };
            }    

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = CA.Id,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            user.UserBalance = user.UserBalance - SP.Price;
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Buy Package) Added!",
                isSuccess = true
            };
        }

        // Đăng tin
        public async Task<InternalTransactionResponse> IT_PostAsync(string UsrId, long? SubPacId)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacId).FirstOrDefault();

            if (SP == null)
            {
                return new InternalTransactionResponse
                {
                    Message = "This Subscription Package Does Not Exits !",
                    isSuccess = false
                };
            }

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.ActionName == "Đăng tin").FirstOrDefault();

            if (CA == null)
            {
                return new InternalTransactionResponse
                {
                    Message = "This Coin Action Does Not Exits !",
                    isSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(UsrId);
            
            if(user.UserBalance == null || user.UserBalance < CA.CaCoinAmount)
            {
                return new InternalTransactionResponse
                {
                    Message = "Not Enough Coin For This Action !",
                    isSuccess = false
                };
            }    

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = CA.Id,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            if(SP.Name=="Free Subscription Package")
            {
                user.UserBalance = user.UserBalance - CA.CaCoinAmount;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }    


            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Post) Added!",
                isSuccess = true
            };
        }

        // Đẩy tin
        public async Task<InternalTransactionResponse> IT_PushUpAsync(string UsrId, long ProductId)
        {
            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.ActionName == "Đẩy tin").FirstOrDefault();

            if (CA == null)
            {
                return new InternalTransactionResponse
                {
                    Message = "This Coin Action Does Not Exits !",
                    isSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(UsrId);

            if (user.UserBalance == null || user.UserBalance < CA.CaCoinAmount)
            {
                return new InternalTransactionResponse
                {
                    Message = "Not Enougn Coin !",
                    isSuccess = false
                };
            }

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = CA.Id,
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

        // Đẩy tin
        public async Task<ProductResponse> SetPriorities( string UsrId, long ProductId)
        {
            var pro = await _context.Products.FindAsync(ProductId);

           
            var ca = await _context.CoinActions.Where(p => p.ActionName == "Đẩy tin").FirstOrDefaultAsync();
            if (ca != null)
            {
                var user = await _userManager.FindByIdAsync(UsrId);

                if(user.UserBalance == null || user.UserBalance < ca.CaCoinAmount)
                {
                    return new ProductResponse
                    {
                        Message = "Not enough coin for this action !",
                        isSuccess = true
                    };
                }
                user.UserBalance = user.UserBalance - ca.CaCoinAmount;

                pro.Priorities = pro.Priorities + 100;
                await _context.SaveChangesAsync();
            }

            return new ProductResponse
            {
                Message = "Product has been pushed up successfully",
                isSuccess = true
            };
        }

        // Lấy danh sách sản phẩm ưu tiên
        public async Task<List<Product>> SortList(List<Product> pro)
        {
            var sortList = pro.OrderByDescending(p => p.Priorities).ToList();
            return sortList;
        }


        // Ẩn tin
        public async Task<ProductResponse> HideProduct(long id)
        {
            var pro = await _context.Products.FindAsync(id);

            if(pro == null)
            {
                return new ProductResponse
                {
                    Message = "This Product Does Not Exits !",
                    isSuccess = false
                };
            }    

            pro.isHidden = true;
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Message = "Product has been hidden successfully",
                isSuccess = true
            };
        }

    }
}
