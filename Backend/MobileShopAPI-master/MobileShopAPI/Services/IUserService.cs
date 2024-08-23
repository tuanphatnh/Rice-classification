using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MobileShopAPI.Services
{
    public interface IUserService
    {

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<UserManagerResponse> ResetEmailAsync(ResetEmailViewModel model);
        Task<UserManagerResponse> ConfirmResetEmailAsync(string userId, string newEmail,string token);
    }


    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if(model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password don't match!",
                    isSuccess = false
                };
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username.Trim()
            };
            var result = await _userManager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                //encode confirm email token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var validEmailToken = EncodeToken(token);
                //create confirm email url with token and user id
                var url = $"{_configuration["AppUrl"]}/api/auth/confirmEmail?userId={user.Id}&token={validEmailToken}";

                string mailBody = "<h4>Confirm your email</h4>"
                + $"<p>Please <a href='{url}'>click here</a> to confirm your email!</p>";

                Message message = new Message(new string[] { model.Email }, "Email Confirmation", mailBody);
                await _emailSender.SendEmailAsync(message);

                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    isSuccess = true
                };
            }


            return new UserManagerResponse
            {
                Message = "Can't create user",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if(user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with this user name",
                    isSuccess = false
                };
            }

            if(user.Status == 1)
            {
                return new UserManagerResponse
                {
                    Message = "User is banned",
                    isSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!result)
            {
                return new UserManagerResponse
                {
                    Message = "Wrong password",
                    isSuccess = false
                };
            }

            var roles = await _userManager.GetRolesAsync(user);


            //Claim list
            List<Claim> claim = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var role in roles)
            {
                var item = new Claim(ClaimTypes.Role, role);
                claim.Add(item);
            }

            //Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSetting:Key"]));

            //Token
            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSetting:Issuer"],
                audience: _configuration["AuthSetting:Audience"],
                claims: claim,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            //send email ===
            string mailBody = "<h1>New login noticed</h1>"
                            + "<p>New login to your account at " + DateTime.Now + "</p>";

            Message message = new Message(new string[] { user.Email }, "New login noticed", mailBody);
            await _emailSender.SendEmailAsync(message);
            //===
            return new UserManagerResponse
            {
                Message = "Login successfully",
                isSuccess = true,
                Token = tokenAsString,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found",
                    isSuccess = false
                };
            }

            //decode confirm email token
            string originalToken = DecodeToken(token);

            var result = await _userManager.ConfirmEmailAsync(user, originalToken);
            if(result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "User email confirmed successfully",
                    isSuccess = true
                };
            return new UserManagerResponse
            {
                Message = "Email did not confirm",
                isSuccess = false,
                Errors = result.Errors.Select(e=>e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found!",
                    isSuccess = false
                };
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var validPasswordResetToken = EncodeToken(token);

            var url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validPasswordResetToken}";

            string mailBody = "<h4>Follow the instruction to reset your password</h4>"
                + $"<p>Please <a href='{url}'>click here</a> to reset your password</p>";

            Message message = new Message(new string[] { email }, "Reset Password", mailBody);
            await _emailSender.SendEmailAsync(message);

            return new UserManagerResponse
            {
                Message = "Reset password URL has been sent successfully",
                isSuccess = true
            };
        }

        public string EncodeToken(string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            return validToken;
        }

        public string DecodeToken(string validToken)
        {
            var encodedToken = WebEncoders.Base64UrlDecode(validToken);
            string originalToken = Encoding.UTF8.GetString(encodedToken);
            return originalToken;
        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found!",
                    isSuccess = false
                };
            }
            if(model.NewPassword != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password does not match!",
                    isSuccess = false
                };
            }
            var originalToken = DecodeToken(model.Token);
            var result = await _userManager.ResetPasswordAsync(user,originalToken,model.NewPassword);
            if(result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    isSuccess = true
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ResetEmailAsync(ResetEmailViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found!",
                    isSuccess = false
                };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Wrong password",
                    isSuccess = false
                };
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user,model.NewEmail);
            var validChangeEmailToken = EncodeToken(token);

            var url = $"{_configuration["AppUrl"]}/api/auth/confirmChangeEmail?userId={user.Id}&newEmail={model.NewEmail}&token={validChangeEmailToken}";

            string mailBody = "<h4>Confirm your new email</h4>"
                + $"<p>Please <a href='{url}'>click here</a> to confirm your new email</p>";

            Message message = new Message(new string[] { model.NewEmail }, "Reset Email", mailBody);
            await _emailSender.SendEmailAsync(message);

            return new UserManagerResponse
            {
                Message = "Reset email URL has been sent successfully",
                isSuccess = true
            };
        }

        public async Task<UserManagerResponse> ConfirmResetEmailAsync(string userId,string newEmail,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found!",
                    isSuccess = false
                };
            }
            var originalToken = DecodeToken(token);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, originalToken);
            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email has been reset successfully!",
                    isSuccess = true
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
