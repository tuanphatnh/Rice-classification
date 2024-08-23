using Microsoft.AspNetCore.Identity;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


public class VnPayLibrary
{
    private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
    private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;



    public VNPayTransactionViewModel GetFullResponseData(IQueryCollection collection, string hashSecret)
    {
        var vnPay = new VnPayLibrary();

        foreach (var (key, value) in collection)
        {
            if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
            {
                vnPay.AddResponseData(key, value);
            }
        }

        string packageId = string.Empty;
        string orderId = string.Empty;
        string transactionInfo = vnPay.GetResponseData("vnp_OrderInfo");
        string[] Infos = transactionInfo.Split("#");
        if (Infos[0].Equals("Order"))
        {
            orderId = Infos[1].ToString();
        }
        else
        {
            packageId = Infos[1].ToString();
        }



        var vnPayTranId = Convert.ToInt64(vnPay.GetResponseData("vnp_TransactionNo"));
        var amount = Convert.ToInt64(vnPay.GetResponseData("vnp_Amount")) / 100;
        var version = vnPay.GetResponseData("vnp_Version");
        var command = vnPay.GetResponseData("vnp_Command");
        var tmnCode = vnPay.GetResponseData("vnp_TmnCode");
        var currentCode = vnPay.GetResponseData("vnp_CurrCode");
        var IpAdd = vnPay.GetResponseData("vnp_IpAddr");
        var locale = vnPay.GetResponseData("vnp_Locale");
        var bankCode = vnPay.GetResponseData("vnp_BankCode");
        var vnpSecureHash = collection.FirstOrDefault(k => k.Key == "vnp_SecureHash").Value; //hash của dữ liệu trả về
        
        var checkSignature =
            vnPay.ValidateSignature(vnpSecureHash, hashSecret); //check Signature

        if (!checkSignature)
            return new VNPayTransactionViewModel()
            {
                Success = false
            };
        
        return new VNPayTransactionViewModel
        {
            Success = true,
            PackageId = packageId,
            Id = vnPayTranId.ToString(),
            VnpSecureHash = vnpSecureHash,
            VnpAmount = amount,
            VnpBankCode =bankCode,
            VnpCommand = command,
            VnpCurrCode = currentCode,
            VnpIpAddr = IpAdd,
            VnpLocale = locale,
            VnpTmnCode = tmnCode,
            OrderId = orderId,
            VnpVersion = version
        };

    }
    public string GetIpAddress(HttpContext context)
    {
        var ipAddress = string.Empty;
        try
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;

            if (remoteIpAddress != null)
            {
                if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                }

                if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                return ipAddress;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "127.0.0.1";
    }
    public void AddRequestData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _requestData.Add(key, value);
        }
    }

    public void AddResponseData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _responseData.Add(key, value);
        }
    }

    public string GetResponseData(string key)
    {
        return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
    }

    public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
    {
        var data = new StringBuilder();

        foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
        {
            data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
        }

        var querystring = data.ToString();

        baseUrl += "?" + querystring;
        var signData = querystring;
        if (signData.Length > 0)
        {
            signData = signData.Remove(data.Length - 1, 1);
        }

        var vnpSecureHash = HmacSha512(vnpHashSecret, signData);
        baseUrl += "vnp_SecureHash=" + vnpSecureHash;

        return baseUrl;
    }

    public bool ValidateSignature(string inputHash, string secretKey)
    {
        var rspRaw = GetResponseData();
        var myChecksum = HmacSha512(secretKey, rspRaw);
        return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }

    private string HmacSha512(string key, string inputData)
    {
        var hash = new StringBuilder();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }

    private string GetResponseData()
    {
        var data = new StringBuilder();
        if (_responseData.ContainsKey("vnp_SecureHashType"))
        {
            _responseData.Remove("vnp_SecureHashType");
        }

        if (_responseData.ContainsKey("vnp_SecureHash"))
        {
            _responseData.Remove("vnp_SecureHash");
        }

        foreach (var (key, value) in _responseData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
        {
            data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
        }

        //remove last '&'
        if (data.Length > 0)
        {
            data.Remove(data.Length - 1, 1);
        }

        return data.ToString();
    }
}

public class VnPayCompare : IComparer<string>
{
    public int Compare(string x, string y)
    {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var vnpCompare = CompareInfo.GetCompareInfo("en-US");
        return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
    }
}