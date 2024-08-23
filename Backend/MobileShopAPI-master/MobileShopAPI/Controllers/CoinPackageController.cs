using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Data;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinPackageController : ControllerBase
    {
        private readonly ICoinPackageService _cpService;

        public CoinPackageController(ICoinPackageService cpService)
        {
            _cpService = cpService;
        }
        /// <summary>
        /// Get all Coin_Package
        /// </summary>
        /// <response code ="200">Get all Coin_Package</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _cpService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get Coin_Package detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Coin_Package infos</response>
        /// <response code ="400">Coin_Package not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var data = await _cpService.GetByIdAsync(id);
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
        /// Add Coin_Package
        /// </summary>
        /// <param name="cp"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Coin Package Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(CoinPackageResponse), 200)]
        [ProducesResponseType(typeof(CoinPackageResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CoinPackageViewModel cp)
        {
            if (ModelState.IsValid)
            {
                var result = await _cpService.AddAsync(cp);
                if (result.isSuccess)
                    return Ok(result); 
                return BadRequest(result);
            }

            return BadRequest("Some properies are not valid");
        }

        /// <summary>
        /// update Coin Package
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cp"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Coin Package Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(CoinPackageResponse), 200)]
        [ProducesResponseType(typeof(CoinPackageResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, CoinPackageViewModel cp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cpService.UpdateAsync(id, cp);
                    if (result.isSuccess)
                        return Ok(result); 
                    return BadRequest(result);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Some properies are not valid");
            }
        }
        /// <summary>
        /// delete Coin Package
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Coin Package Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(CoinPackageResponse), 200)]
        [ProducesResponseType(typeof(CoinPackageResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cpService.DeleteAsync(id);
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
