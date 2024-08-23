using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using EmailService;
using Swashbuckle.Swagger.Annotations;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IActiveSubscriptionService _activeSubsciptionService;
        public AuthController(IUserService userService,IEmailSender mailService, IActiveSubscriptionService activeSubscriptionService)
        {
            _userService = userService;
            _activeSubsciptionService = activeSubscriptionService;
        }
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>Heyyyyyyyyy</remarks>
        /// <response code ="200">Account created</response>
        /// <response code ="400">Account has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
               
                if(result.isSuccess)
                {
                    var asub = await _activeSubsciptionService.RegisterActiveSubAsync(model);
                    return Ok(result); //Status code: 200
                }    
                    
                return BadRequest(result);//Status code: 400
            }

            return BadRequest("Some properies are not valid");//Status code: 400
        }


        ///// <summary>
        ///// Register a user
        ///// </summary>
        ///// <param name="model"></param>
        ///// <remarks>Heyyyyyyyyy</remarks>
        ///// <response code ="200">Account created</response>
        ///// <response code ="400">Account has missing/invalid values</response>
        ///// <response code ="500">>Oops! Something went wrong</response>
        //// api/auth/register
        //[HttpPost("register_google")]
        //[ProducesResponseType(typeof(UserManagerResponse), 200)]
        //[ProducesResponseType(typeof(UserManagerResponse), 400)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> RegisterGoogleAsync(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.RegisterUserAsync(model);
        //        if (result.isSuccess)
        //            return Ok(result); //Status code: 200
        //        return BadRequest(result);//Status code: 400
        //    }

        //    return BadRequest("Some properies are not valid");//Status code: 400
        //}



        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code ="200">User logged in!</response>
        /// <response code ="400">Account has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.isSuccess)
                {
                    return Ok(result); //Status code: 200
                }
                    
                return BadRequest(result);//Status code: 400
            }
            return BadRequest("Some properies are not valid");//Status code: 400
        }
        /// <summary>
        /// Confirm user email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <response code ="200">Your email has been confirmed successfully!</response>
        /// <response code ="400">Token invalid</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/confirmEmail
        [HttpGet("confirmEmail")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Not Found");
            }
            var result = await _userService.ConfirmEmailAsync(userId, token);
            if(result.isSuccess)
            {
                return Ok(result);//200
            }
            return BadRequest(result);//400
        }
        /// <summary>
        /// Confirm new email of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newEmail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <response code ="200">Your email has been confirmed successfully!</response>
        /// <response code ="400">Token invalid</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/confirmChangeEmail
        [HttpGet("confirmChangeEmail")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ConfirmChangeEmail(string userId, string newEmail, string token)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ConfirmResetEmailAsync(userId, newEmail, token);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properies are not valid");//Status code: 400
        }
        /// <summary>
        /// Send reset password Url with security token to user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code ="200">Reset password url has been sent successfully!</response>
        /// <response code ="400">User not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/forgetPassword
        [HttpPost("forgetPassword")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Not Found");
            }
            var result = await _userService.ForgetPasswordAsync(email);
            if (result.isSuccess)
            {
                return Ok(result);//200
            }
            return BadRequest(result);//400
        }
        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code ="200">Password has been reset successfully!</response>
        /// <response code ="400">Password does not match/token invalid</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/resetPassword
        [HttpPost("resetPassword")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);
                if(result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properies are not valid");//Status code: 400
        }
        /// <summary>
        /// Reset user Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code ="200">Email has been changed successfully!</response>
        /// <response code ="400">Token invalid</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        // api/auth/changeEmail
        [HttpPost("changeEmail")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        [ProducesResponseType(typeof(UserManagerResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ChangeEmail(ResetEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetEmailAsync(model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properies are not valid");//Status code: 400
        }

    }
}
