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
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        /// <summary>
        /// Get all Color
        /// </summary>
        /// <response code ="200">Get all color</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _colorService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get color detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Color infos</response>
        /// <response code ="400">Color not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _colorService.GetByIdAsync(id);
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
        /// Add Color
        /// </summary>
        /// <param name="color"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Color Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ColorResponse), 200)]
        [ProducesResponseType(typeof(ColorResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(ColorViewModel color)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _colorService.AddAsync(color);
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
        /// update color
        /// </summary>
        /// <param name="id"></param>
        /// <param name="color"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Color Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ColorResponse), 200)]
        [ProducesResponseType(typeof(ColorResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, ColorViewModel color)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _colorService.UpdateAsync(id,color);
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
        /// delete color
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Color Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ColorResponse), 200)]
        [ProducesResponseType(typeof(ColorResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _colorService.DeleteAsync(id);
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
