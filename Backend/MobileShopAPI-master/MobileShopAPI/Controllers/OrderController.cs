using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all Order
        /// </summary>
        /// <response code ="200">Get all Order</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<Order>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var ordeList = await _orderService.GetAllOrderAsync();
            return Ok(ordeList);
        }

        /// <summary>
        /// Get order detail by userid
        /// </summary>
        /// <param name="id"></param>
        /// <response code ="200">order  infos</response>
        /// <response code ="400">order  not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>

        [HttpGet("getListOrderByUser")]
        [ProducesResponseType(typeof(List<Order>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetListByUser(string id)
        {
            var ordeList = await _orderService.GetListOrderByUser(id);
            return Ok(ordeList);
        }


        /// <summary>
        /// Get order detail 
        /// </summary>
        /// <param name="id"></param>
        /// <response code ="200">order  infos</response>
        /// <response code ="400">order  not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("GetOrderDetail")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        { 
            try
            {
                var order = await _orderService.GetOrderDetailAsync(id);
                if (order != null)
                {
                    return Ok(order);
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

        //[HttpGet("getListBuyerByUser")]
        //public async Task<IActionResult> GetListBuyerByUser(string id)
        //{

        //    try
        //    {
        //       var ordeList = await _orderService.GetListBuyerByUser(id);
        //        if (ordeList != null)
        //        {
        //            return Ok(ordeList);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}



        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Shipping Address has been added successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(typeof(OrderResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.CreateOrderAsync(model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        /// <summary>
        /// update order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">order has been updated successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("edit/{Id}")]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(typeof(OrderResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(string id, OrderUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.UpdateOrderAsync(id, model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        /// <summary>
        /// update order
        /// </summary>
        /// <param name="id"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">order has been updated successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{Id}")]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(typeof(OrderResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.DeleteOrderAsync(id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
