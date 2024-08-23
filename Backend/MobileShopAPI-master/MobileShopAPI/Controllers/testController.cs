using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Helpers;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        /// <summary>
        /// Create for testing only, front-end group can ignore this
        /// </summary>
        [HttpGet("AuthorizeTest")]
        [Authorize(Roles = "Admin")]
        public IActionResult AuthTest()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if(user!=null)
                return Ok(user.Value);
            return BadRequest();
        }
        /// <summary>
        /// Create for testing only, front-end group can ignore this
        /// </summary>
        [HttpGet]
        [Route("getBaseUrl")]
        public string GetBaseUrl()
        {
            var request = HttpContext.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}:{request.PathBase.ToUriComponent()}";

            var confirmEmailUrl = $"{baseUrl}/api/auth/confirmEmail";
            return baseUrl;
        }
        /// <summary>
        /// Created for testing only, front-end group can ignore this
        /// </summary>
        [HttpGet]
        [Route("getUniqueString")]
        public string GetUniqueString()
        {
            return StringIdGenerator.GenerateUniqueId();
        }
    }
}
