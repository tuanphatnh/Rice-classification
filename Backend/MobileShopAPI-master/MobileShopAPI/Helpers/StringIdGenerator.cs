using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace MobileShopAPI.Helpers
{
    public class StringIdGenerator
    {
        public static string GenerateUniqueId()
        {
            long uniqueNumber = DateTime.Now.Ticks;
            string uniqueString = Convert.ToBase64String(BitConverter.GetBytes(uniqueNumber));
            string validString = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(uniqueString));
            return validString;
        }
    }
}
