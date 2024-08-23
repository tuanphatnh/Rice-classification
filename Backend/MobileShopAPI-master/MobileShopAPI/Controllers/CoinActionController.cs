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
    public class CoinActionController : ControllerBase
    {
        private readonly ICoinActionService _caService;

        public CoinActionController(ICoinActionService caService)
        {
            _caService = caService;
        }

        /// <summary>
        /// Get all CoinAction
        /// </summary>
        /// <response code ="200">Get all CoinAction</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _caService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get CoinAction detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Coin Action infos</response>
        /// <response code ="400">Coin Action not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _caService.GetByIdAsync(id);
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
        /// Add CoinAction
        /// </summary>
        /// <param name="ca"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Coin_Action Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(BrandResponse), 200)]
        [ProducesResponseType(typeof(BrandResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CoinActionViewModel ca)
        {
            if (ModelState.IsValid)
            {
                var result = await _caService.AddAsync(ca);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
        /// <summary>
        /// update CoinAction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ca"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Coin_Action Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(CoinActionResponse), 200)]
        [ProducesResponseType(typeof(CoinActionResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, CoinActionViewModel ca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _caService.UpdateAsync(id, ca);
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
        /// delete Coin Action
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Coin_Action Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(CoinActionResponse), 200)]
        [ProducesResponseType(typeof(CoinActionResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _caService.DeleteAsync(id);
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
