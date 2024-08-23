using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Get all report
        /// </summary>
        /// <response code ="200">Get all report</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<Report>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _reportService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all report of user by userid
        /// </summary>
        /// <param name="userId"></param>
        /// <remarks>Get Id of user"</remarks>
        /// <response code ="200">Report infos</response>
        /// <response code ="400">Report of user not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAllReportOfUser/{userId}")]
        [ProducesResponseType(typeof(Report), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllReportOfUser(string userId)
        {
            try
            {
                var data = await _reportService.GetAllReportOfUserAsync(userId);
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
        /// Show all reported information of users by userid
        /// </summary>
        /// <param name="reportedUserId"></param>
        /// <remarks>Get Id of reported user"</remarks>
        /// <response code ="200">reported infos of user</response>
        /// <response code ="400">Reported infos of user not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("GetAllReportedInfoOfUser/{reportedUserId}")]
        [ProducesResponseType(typeof(Report), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllReportedInfoOfUser(string reportedUserId)
        {
            try
            {
                var data = await _reportService.GetAllReportedInfoOfUserAsync(reportedUserId);
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
        /// Show all reported information of product by productid
        /// </summary>
        /// <param name="reportedProductId"></param>
        /// <remarks>Get Id of product"</remarks>
        /// <response code ="200">reported infos of product</response>
        /// <response code ="400">Reported infos of product not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("GetAllReportedInfoOfProduct/{reportedProductId}")]
        [ProducesResponseType(typeof(Report), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllReportedInfoOfProduct(long reportedProductId)
        {
            try
            {
                var data = await _reportService.GetAllReportedInfoOfProductAsync(reportedProductId);
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
        /// Get report detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Get Id of user</remarks>
        /// <response code ="200">Report infos</response>
        /// <response code ="400">Report not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(Report), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _reportService.GetByIdAsync(id);
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
        /// Add report
        /// </summary>
        /// <param name="report"></param>
        /// <remarks>Add report of user</remarks>
        /// <returns></returns>
        /// <response code ="200">Report has been added successfully</response>
        /// <response code ="404">Report has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ReportResponse), 200)]
        [ProducesResponseType(typeof(ReportResponse), 404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(ReportViewModel report)
        {
            if (ModelState.IsValid)
            {
                var result = await _reportService.AddAsync(report);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }

        /// <summary>
        /// update report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Report has been updated successfully</response>
        /// <response code ="404">Report has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ReportResponse), 200)]
        [ProducesResponseType(typeof(ReportResponse), 404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(long id, ReportViewModel report)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _reportService.UpdateAsync(id, report);
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
        /// delete report
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>delete report and evidence<remarks>
        /// <returns></returns>
        /// <response code ="200">Report has been deleted successfully</response>
        /// <response code ="404">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ReportResponse), 200)]
        [ProducesResponseType(typeof(ReportResponse), 404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _reportService.DeleteAsync(id);
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
