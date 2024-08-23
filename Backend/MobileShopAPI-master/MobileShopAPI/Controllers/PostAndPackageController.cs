using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAndPackageController : ControllerBase
    {

        private readonly IPostAndPackageService _PaPService;

        public PostAndPackageController(IPostAndPackageService PaPService)
        {
            _PaPService = PaPService;
        }

        /// <summary>
        /// Get all product by priorities
        /// </summary>
        /// <response code ="200">Get all product</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("pushUp/{id}")]
        [Authorize]
        public async Task<IActionResult> PushUp(long id)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                var result = await _PaPService.SetPriorities(user.Value, id);

                var in_tran = await _PaPService.IT_PushUpAsync(user.Value, id);

                //var active_sub = await _PaPService.AS_PushUpAsync(user.Value, model.SpId);

                if (result.isSuccess)
                {
                    if(in_tran.isSuccess)
                    {
                        return Ok(result);
                    }    
                }    
                    
                return BadRequest(result);
            }

            return BadRequest("Some properies are not valid");
        }

        ///// <summary>
        ///// Get all product by priorities
        ///// </summary>
        ///// <response code ="200">Get all product</response>
        ///// <response code ="500">>Oops! Something went wrong</response>
        //[HttpGet("getAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        return Ok(await _PaPService.SortList());
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        /// <summary>
        /// Buy Subscription Package
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">New Active Subscription Added</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        //[HttpPost("buyPackage")]
        //[ProducesResponseType(typeof(ActiveSubscriptionResponse), 200)]
        //[ProducesResponseType(typeof(ActiveSubscriptionResponse), 400)]
        //[ProducesResponseType(500)]
        //[Authorize]
        //public async Task<IActionResult> BuyPackage(AddActiveSubscriptionViewModel model)
        //{
        //    var user = User.FindFirst(ClaimTypes.NameIdentifier);

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _PaPService.AS_BuyPackageAsync(model, user.Value);

        //        var in_tran = await _PaPService.IT_BuyPackageAsync(user.Value, model.SpId);

        //        if (in_tran.isSuccess)
        //        {
        //            if(result.isSuccess)
        //            { return Ok(result); }
        //        }    
        //        return BadRequest(in_tran);
        //    }

        //    return BadRequest("Some properies are not valid");
        //}

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been added successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        //[HttpPost("post")]
        //[ProducesResponseType(typeof(ProductResponse), 200)]
        //[ProducesResponseType(typeof(ProductResponse), 400)]
        //[ProducesResponseType(500)]
        //[Authorize]
        //public async Task<IActionResult> Post(ProductSpIdViewModel model)
        //{
        //    var user = User.FindFirst(ClaimTypes.NameIdentifier);

        //    if (ModelState.IsValid)
        //    {

        //        var rs = await _PaPService.IT_PostAsync(user.Value, model.SpId);

        //        var asub = await _PaPService.AS_PostAsync(model.SpId, user.Value);

        //        if (rs.isSuccess && asub.isSuccess)
        //        {
        //            var result = await _PaPService.CreateProductAsync(model, user.Value);
        //            if(result.isSuccess)
        //                return Ok(result);
        //        }
        //        return BadRequest(rs);
        //    }

        //    return BadRequest("Some properties are not valid");
        //}

        /// <summary>
        /// Hide Product
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Hide product successfully</response>
        /// <response code ="400">Product not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("hideProduct/{id}")]
        public async Task<IActionResult> HideProduct(long id)
        {
            try
            {
                var data = await _PaPService.HideProduct(id);
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
    }
}
