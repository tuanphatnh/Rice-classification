using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ApplicationDbContext _context;
        private readonly EmailService.IEmailSender _emailSender;
        
        public PaymentController(IVnPayService vnPayService, ApplicationDbContext context,UserManager<ApplicationUser> userManager, 
            EmailService.IEmailSender emailSender)
        {
            _vnPayService = vnPayService;
            _context = context;
            _user = userManager;
            _emailSender = emailSender;
            
        }


        /// <summary>
        /// Get payment link
        /// </summary>
        /// <response code ="200">Get payment link </response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost]
        public async Task<IActionResult>CreatePaymentUrl([FromBody]PaymentInformationModel model) 
        {
            var user = await _user.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_user.GetUserId(User)}'.");
            }
            var order = await _context.Orders.Where(a => a.Id == model.OrderId ).SingleOrDefaultAsync();

            var package = await _context.CoinPackages.Where(a => a.Id == model.packageId ).SingleOrDefaultAsync();

            if (order != null || package != null)
            {
                var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
                
                return Json(url);
            }
            return BadRequest();
        }
        [HttpGet("callback")]
        public async Task<IActionResult>PaymentCallback()
        {
            var user = await _user.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_user.GetUserId(User)}'.");
            }

            
            var response = _vnPayService.PaymentExecute(Request.Query);
            
            VnpTransaction transaction = new()
            {
                UserId = user.Id,
                Id = StringIdGenerator.GenerateUniqueId(),
                PackageId = response.PackageId,
                OrderId = response.OrderId,
                VnpAmount = response.VnpAmount,
                VnpBankCode = response.VnpBankCode,
                VnpCommand = response.VnpCommand,
                VnpCreateDate = response.VnpCreateDate,
                VnpCurrCode = response.VnpCurrCode,
                VnpIpAddr = response.VnpIpAddr,
                VnpLocale = response.VnpLocale,
                VnpSecureHash = response.VnpSecureHash,
                VnpTmnCode  = response.VnpTmnCode,
                VnpVersion  = response.VnpVersion
            };

            if (transaction.OrderId == "") transaction.OrderId = null;
            if (transaction.PackageId == "") transaction.PackageId = null;

            if (transaction != null && transaction.OrderId != null)
            {
                Order order = await _context.Orders.FindAsync(transaction.OrderId);
                if(order != null)
                {
                    order.Status = 1;
                    order.UpdateDate = DateTime.Now;
                    _context.Orders.Update(order);
                    await _context.SaveChangesAsync();
                    String message = "<p>Xin chào " + order.UserFullName + ",</p>" +
                                "<p><b>Chi tiết đơn hàng</b> :</p>" +
                                "<p><b>Địa chỉ giao</b> : " + order.Address + "</p>" +
                                "<p><b>Ngày thanh toán</b> : " + order.CreatedDate + "</p>" +
                                "<p><b>Mã giao dịch</b> : " + transaction.Id + "</p>" +
                                "<p><b>Tổng giá trị </b> : " + order.Total + "</p>";
                    EmailService.Message mssg = new EmailService.Message(new string[] { user.Email }, "Chi tiết đơn hàng", message);
                    await _emailSender.SendEmailAsync(mssg);
                }
            }
            if(transaction != null && transaction.PackageId != null)
            {

                CoinPackage package = await _context.CoinPackages.FindAsync(transaction.PackageId);
                if(package != null)
                {
                    if(user.UserBalance == null) { user.UserBalance = 0; }
                    user.UserBalance += package.PackageValue;
                    user.UpdatedDate = DateTime.Now;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    String message = "<p>Xin chào " + user.FirstName + ",</p>" +
                                "<p><b>Chi tiết đơn hàng</b> :</p>" +
                                "<p><b>Tên gói nạp</b> : " + package.PackageName + "</p>" +
                                "<p><b>Ngày nạp</b> : " + user.CreatedDate + "</p>" +
                                "<p><b>Mã giao dịch</b> : " + transaction.Id + "</p>" +
                                "<p><b>Giá trị </b> : " + package.PackageValue + "</p>";
                    EmailService.Message mssg = new EmailService.Message(new string[] { user.Email }, "Chi tiết đơn hàng", message);
                    await _emailSender.SendEmailAsync(mssg);
                }

            }

            if (transaction != null)
                _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Json(true);

        }
        
        
    }
    


}
