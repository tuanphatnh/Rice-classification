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
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        /// <summary>
        /// Get all size
        /// </summary>
        /// <response code ="200">Get all size</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _sizeService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Get size detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Size infos</response>
        /// <response code ="400">Size not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _sizeService.GetByIdAsync(id);
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
        /// Add size
        /// </summary>
        /// <param name="size"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Size Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(BrandResponse), 200)]
        [ProducesResponseType(typeof(BrandResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(SizeViewModel size)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.AddAsync(size);
                    if (result.isSuccess)
                        return Ok(result); 
                    return BadRequest(result);
                }

                return BadRequest("Some properies are not valid");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// update size
        /// </summary>
        /// <param name="id"></param>
        /// <param name="size"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Size Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(SizeResponse), 200)]
        [ProducesResponseType(typeof(SizeResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, SizeViewModel size)
        {
            try
            {
                //_sizeService.UpdateAsync(size);
                //return NoContent();

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.UpdateAsync(id,size);
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
        /// delete size
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Size Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(SizeResponse), 200)]
        [ProducesResponseType(typeof(SizeResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                //_sizeService.DeleteAsync(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.DeleteAsync(id);
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
