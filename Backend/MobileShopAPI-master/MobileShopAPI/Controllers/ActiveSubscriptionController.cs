using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Security.Claims;
using System.Web.Http.ModelBinding;

namespace MobileShopAPI.Controllers
{
    public class ActiveSubscriptionController : ControllerBase
    {
        private readonly IActiveSubscriptionService _asService;

        private readonly IInternalTransactionService _transactionService;

        public ActiveSubscriptionController(IActiveSubscriptionService asService, IInternalTransactionService transactionService)
        {
            _asService = asService;
            _transactionService = transactionService;
        }
        /// <summary>
        /// Get all Active Subscription
        /// </summary>
        /// <response code ="200">Get all ActiveSubscription</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _asService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Active Subscription detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Active Subscripton infos</response>
        /// <response code ="400">Active Subscription not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _asService.GetByIdAsync(id);
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
        /// Get Active Subscription List by UserId
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Active Subscripton infos</response>
        /// <response code ="400">Active Subscription not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getByUserId/{id}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            try
            {
                var data = await _asService.GetByUserIdAsync(id);
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
        /// Get Active Subscription List by User Logged
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Active Subscripton infos</response>
        /// <response code ="400">Active Subscription not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getByUserIdLogged")]
        public async Task<IActionResult> GetByUserIdLogged()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = user.Value;
            try
            {
                var data = await _asService.GetByUserIdAsync(userId);
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
        /// Get Active Subscription List By SpId
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Active Subscripton infos</response>
        /// <response code ="400">Active Subscription not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getBySubId/{id}")]
        public async Task<IActionResult> GetBySubId(long id)
        {
            try
            {
                var data = await _asService.GetBySubIdAsync(id);
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
        /// Get Active Subscription List By SpId
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Active Subscripton infos</response>
        /// <response code ="400">Active Subscription not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getBySubIdAndUserId")]
        public async Task<IActionResult> GetBySubIdAndUserId(ActiveSubscriptionViewModel model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            try
            {
                var data = await _asService.GetBySubIdAndUserIdAsync(model.SpId, user.Value);
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
        /// Buy Subscription Package
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Active Subscription Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ActiveSubscriptionResponse), 200)]
        [ProducesResponseType(typeof(ActiveSubscriptionResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(AddActiveSubscriptionViewModel model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                var result = await _asService.BuyPackageAsync(model, user.Value);

                var in_tran = await _transactionService.BuyPackageAsync(user.Value, model.SpId);

                if (result.isSuccess)
                    return Ok(result);
                return BadRequest(result);
            }

            return BadRequest("Some properies are not valid");
        }

        /// <summary>
        /// update Active Subscription
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Active Subscription Updated</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(BrandResponse), 200)]
        [ProducesResponseType(typeof(BrandResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(long id, ActiveSubscriptionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _asService.UpdateAsync(id, model);
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
        /// delete Active Subscription
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">This Active Subscription Deleted</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ActiveSubscriptionResponse), 200)]
        [ProducesResponseType(typeof(ActiveSubscriptionResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _asService.DeleteAsync(id);
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
