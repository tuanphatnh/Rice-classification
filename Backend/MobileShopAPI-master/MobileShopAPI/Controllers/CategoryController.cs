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

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _cateService;

        public CategoryController(ICategoryService cateService)
        {
            _cateService = cateService;
        }

        /// <summary>
        /// Get all categogies
        /// </summary>
        /// <response code ="200">Get all categogies</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _cateService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get category detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Category infos</response>
        /// <response code ="400">Category not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _cateService.GetByIdAsync(id);
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
        /// Add Category
        /// </summary>
        /// <param name="cate"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Category Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(CategoryResponse), 200)]
        [ProducesResponseType(typeof(CategoryResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CategoryViewModel cate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cateService.AddAsync(cate);
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
        /// update category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cate"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Category Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(CategoryResponse), 200)]
        [ProducesResponseType(typeof(CategoryResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, CategoryViewModel cate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cateService.UpdateAsync(id,cate);
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
        /// delete category
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Category Deleted</response>
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
                //_cateService.DeleteAsync(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _cateService.DeleteAsync(id);
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
