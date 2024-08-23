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
    public class UserRatingController : ControllerBase
    {
        private readonly IUserRatingService _userRatingService;

        public UserRatingController(IUserRatingService userRatingService)
        {
            _userRatingService = userRatingService;
        }

        /// <summary>
        /// Get all UserRating
        /// </summary>
        /// <response code ="200">Get all UserRaiting</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<UserRating>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userRatingService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get UserRaiting detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">UserRaiting infos</response>
        /// <response code ="400">UserRaiting not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(List<UserRating>), 200)]
        [ProducesResponseType(typeof(List<UserRating>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _userRatingService.GetByIdAsync(id);
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
        /// Get UserRaiting's List
        /// </summary>
        /// <param name="id"></param>
        /// <remarks> Return UserRaiting's List Of This UserId </remarks>
        /// <returns></returns>
        /// <response code ="200">UserRaiting infos</response>
        /// <response code ="400">UserRaiting not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getByUserId/{id}")]
        [ProducesResponseType(typeof(UserRating), 200)]
        [ProducesResponseType(typeof(UserRating), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByUserId(string id)
        {
            try
            {
                var data = await _userRatingService.GetAllRatingOfUserAsync(id);
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
        /// Add UserRaiting
        /// </summary>
        /// <param name="userRating"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New User Raiting Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(UserRatingResponse), 200)]
        [ProducesResponseType(typeof(UserRatingResponse), 400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> Add(UserRatingViewModel userRating)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                var result = await _userRatingService.AddAsync(user.Value, userRating);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
        /// <summary>
        /// update User Raiting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRating"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This User Raiting Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(UserRatingResponse), 200)]
        [ProducesResponseType(typeof(UserRatingResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(long id, UserRatingViewModel userRating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRatingService.UpdateAsync(id, userRating);
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
        /// delete UserRating
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This User Raiting Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(UserRatingResponse), 200)]
        [ProducesResponseType(typeof(UserRatingResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRatingService.DeleteAsync(id);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200 ...
                    return BadRequest(result);//Status code: 404
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Everage Raiting Of Product
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        [HttpGet("getAverageRating")]
        public async Task<float> getAverageRating(long id)
        {
            var result = await _userRatingService.getAverageRatingAsync(id);

            return result;
        }
    }
}
