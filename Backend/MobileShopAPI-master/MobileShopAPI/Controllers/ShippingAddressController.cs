using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingaddressService;

        public ShippingAddressController(IShippingAddressService shippingaddressService)
        {
           _shippingaddressService = shippingaddressService;
        }

        /// <summary>
        /// Get user addresses
        /// </summary>
        /// <response code ="200">Get all user's shipping addresses</response>
        /// <response code ="500">>Oops! Something went wrong</response>

        [HttpGet("getUserAddress")]
        [ProducesResponseType(typeof(List<ShippingAddress>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (user != null)
            {
                userId = user.Value;
                var addressList = await _shippingaddressService.GetAllUserAddressAsync(userId);
                return Ok(addressList);
            }
            return BadRequest("User not authorized");
        }

        /// <summary>
        /// Get Shipping Address detail
        /// </summary>
        /// <param name="id"></param>
        /// <response code ="200">Shipping address infos</response>
        /// <response code ="400">Shipping address not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>

        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(ShippingAddressViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _shippingaddressService.GetByIdAsync(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add Shipping Address
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Shipping Address has been added successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>

        [HttpPost("create")]
        [ProducesResponseType(typeof(ShippingAddressResponse), 200)]
        [ProducesResponseType(typeof(ShippingAddressResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(ShippingAddressViewModel model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (user == null)
            {
                return BadRequest("User not authorized");
            }

            userId = user.Value;

            if (ModelState.IsValid)
            {
                model.UserId = userId;
                var result = await _shippingaddressService.CreateAddressAsync(model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        /// <summary>
        /// Update Shipping Address
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Shipping Address has been added successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ShippingAddressResponse), 200)]
        [ProducesResponseType(typeof(ShippingAddressResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(long addressId, ShippingAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _shippingaddressService.UpdateAddressAsync(addressId, model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        /// <summary>
        /// delete Shipping address
        /// </summary>
        /// <param name="id"></param>

        /// <returns></returns>
        /// <response code ="200">Shipping address has been deleted successfully</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ShippingAddressResponse), 200)]
        [ProducesResponseType(typeof(ShippingAddressResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _shippingaddressService.DeleteAddressAsync(id);
                    if (result.isSuccess)
                        return Ok(result); 
                    return BadRequest(result);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
