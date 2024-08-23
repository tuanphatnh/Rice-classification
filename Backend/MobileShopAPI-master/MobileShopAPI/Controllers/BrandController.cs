using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Data;


namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        /// <summary>
        /// Get all brand
        /// </summary>
        /// <response code ="200">Get all brand</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _brandService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get brand detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Brand infos</response>
        /// <response code ="400">Brand not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _brandService.GetByIdAsync(id);
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
        /// Add brand
        /// </summary>
        /// <param name="brand"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Brand Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(BrandResponse), 200)]
        [ProducesResponseType(typeof(BrandResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var result = await _brandService.AddAsync(brand);
                if (result.isSuccess)
                    return Ok(result);
                return BadRequest(result);
            }

            return BadRequest("Some properies are not valid");
        }

        /// <summary>
        /// update brand
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brand"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Brand Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(BrandResponse), 200)]
        [ProducesResponseType(typeof(BrandResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, BrandViewModel brand)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _brandService.UpdateAsync(id,brand);
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
        /// delete brand
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Brand Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _brandService.DeleteAsync(id);
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
