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
    public class ReportCategoryController : ControllerBase
    {
        private readonly IReportCategoryService _categoryService;

        public ReportCategoryController(IReportCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all report category
        /// </summary>
        /// <response code ="200">Get all report category</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<ReportCategory>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _categoryService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get report category detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Get Id of report category"</remarks>
        /// <response code ="200">Report category infos</response>
        /// <response code ="400">Report category not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(ReportCategory), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _categoryService.GetByIdAsync(id);
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
        /// Add report category
        /// </summary>
        /// <param name="reportCategory"></param>
        /// <remarks>Add report category</remarks>
        /// <returns></returns>
        /// <response code ="200">Report category has been added successfully</response>
        /// <response code ="404">Report category has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ReportCategoryResponse), 200)]
        [ProducesResponseType(typeof(ReportCategoryResponse), 404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(ReportCategoryViewModel reportCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(reportCategory);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }

        /// <summary>
        /// update report category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reportCategory"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Report category has been updated successfully</response>
        /// <response code ="404">Report category has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ReportCategoryResponse), 200)]
        [ProducesResponseType(typeof(ReportCategoryResponse), 404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, ReportCategoryViewModel reportCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.UpdateAsync(id, reportCategory);
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
        /// delete report category
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>delete report catgory<remarks>
        /// <response code ="200">Report category has been deleted successfully</response>
        /// <response code ="404">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ReportCategoryResponse), 200)]
        [ProducesResponseType(typeof(ReportCategoryResponse), 404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.DeleteAsync(id);
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
