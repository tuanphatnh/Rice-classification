using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Data;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPackageController : ControllerBase
    {
        private readonly ISubscriptionPackageService _spService;

        public SubscriptionPackageController(ISubscriptionPackageService spService)
        {
            _spService = spService;
        }

        /// <summary>
        /// Get all Subscription Package
        /// </summary>
        /// <response code ="200">Get all Subscription Package</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _spService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Subscription detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Subscription Package infos</response>
        /// <response code ="400">Subscription Package not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _spService.GetByIdAsync(id);
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
        /// Add Subsciption Package
        /// </summary>
        /// <param name="sp"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Subscription Package Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(SubscriptionPackageResponse), 200)]
        [ProducesResponseType(typeof(SubscriptionPackageResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(SubscriptionPackageViewModel sp)
        {
            if (ModelState.IsValid)
            {
                var result = await _spService.AddAsync(sp);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
        /// <summary>
        /// update Subscription Package
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sp"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Subscription Package Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(SubscriptionPackageResponse), 200)]
        [ProducesResponseType(typeof(SubscriptionPackageResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, SubscriptionPackageViewModel sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _spService.UpdateAsync(id, sp);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Some properies are not valid");//Status code: 404
            }
        }

        /// <summary>
        /// update Subscription Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sp"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Status Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        [HttpPut("updateStatus/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(long id, SubscriptionPackageStatusViewModel sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _spService.UpdateStatusAsync(id, sp);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Some properies are not valid");//Status code: 404
            }
        }

        /// <summary>
        /// delete Subscription Package
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Subscription Package Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(SubscriptionPackageResponse), 200)]
        [ProducesResponseType(typeof(SubscriptionPackageResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _spService.DeleteAsync(id);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
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
