using Microsoft.AspNetCore.Identity;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MobileShopAPI.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        VNPayTransactionViewModel PaymentExecute(IQueryCollection collections);

        

    }

    public class VnPayService : IVnPayService 
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _user;
        public VnPayService(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> user)
        {
            _configuration = configuration;
            _context = context;
            _user = user;
        }
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            var ordervalue = _context.Orders.FirstOrDefault(o => o.Id == model.OrderId);
            var packagevalue = _context.CoinPackages.FirstOrDefault(o => o.Id == model.packageId);

            string orderInfo = string.Empty;

            long amount = 0;
            
            if(ordervalue != null )
            {
                amount = ordervalue.Total;
                orderInfo = "Order#" + ordervalue.Id;
            }
            else if(packagevalue != null)
            {
                amount = packagevalue.PackageValue;
                orderInfo = "CoinPackage#" + packagevalue.Id; 
            }

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int) amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{orderInfo}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", StringIdGenerator.GenerateUniqueId());
            

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
            

            return paymentUrl;
        }

        public VNPayTransactionViewModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            return response;
        }

       
        
    }
}
