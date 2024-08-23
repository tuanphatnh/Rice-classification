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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPostAndPackageService _postAndPackageService;

        public ProductController(IProductService productService, IPostAndPackageService postAndPackageService)
        {
            _productService = productService;
            _postAndPackageService = postAndPackageService;
        }
        /// <summary>
        /// Get all non-hidden product
        /// </summary>
        /// <remarks>Product is hidden if "Status = 2 , 3" or "isHidden = true"</remarks>
        /// <response code ="200">Get all non-hidden product</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll(int page, int size)
        {
            var productList = await _productService.GetAllNoneHiddenProductAsync();

            // Sort list
            List<Product> sortedList = await _postAndPackageService.SortList(productList);

            if (page < 1)
            {
                page = 1;
            }

            int recsCount = sortedList.Count();

            var pager = new Pager(recsCount, page, size);

            var recSkip = (page - 1) * size;

            var data = new
            {
                ProductList = sortedList.Skip(recSkip).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            return Ok(data);
        }
        /// <summary>
        /// Get all product
        /// </summary>
        /// <remarks>Only admin can access this API</remarks>
        /// <response code ="200">Get all product</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("AdminGetAll")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AdminGetAll()
        {
            var productList = await _productService.GetAllProductAsync();
            return Ok(productList);
        }
        /// <summary>
        /// Get non-hidden product detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Product is hidden if "Status = 2 , 3" or "isHidden = true"</remarks>
        /// <returns></returns>
        /// <response code ="200">Product infos</response>
        /// <response code ="400">Product not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(ProductDetailViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ProductDetail(long id)
        {
            var product = await _productService.GetNoneHiddenProductDetailAsync(id);
            if(product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        /// <summary>
        /// Get product detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Only admin can access this API</remarks>
        /// <returns></returns>
        /// <response code ="200">Product infos</response>
        /// <response code ="400">Product not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("AdminGetById/{id}")]
        [ProducesResponseType(typeof(ProductDetailViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AdminProductDetail(long id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            if (product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Add image along with product:
        /// 
        ///     GET
        ///     {
        ///         "id": 0, (not require, can be any number but null)
        ///         "url": "string", (required)
        ///         "isCover": false, (not required, true/false makes no differences)
        ///         "isVideo" : false, (whether or not this object is a video)
        ///         "isDeleted": false, (not required, true/false makes no differences)
        ///         "isNewlyAdded": false (not required, true/false makes no differences)
        ///     }
        ///</remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been added successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse),400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Some properties are not valid");
            }



            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (user != null)
            {
                userId = user.Value;
                var rs = await _postAndPackageService.IT_PostAsync(user.Value, model.SpId);

                var asub = await _postAndPackageService.AS_PostAsync(model.SpId, user.Value);


                if (!(rs.isSuccess && asub.isSuccess))
                    return BadRequest(rs);
                var result = await _productService.CreateProductAsync(userId, model);
                if (result.isSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
                
            return BadRequest("Oops! Something went wrong");
        }

        /// <summary>
        /// Approve product
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Approve product, change status to 0 and isHidden to false</remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been approved</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("ApproveProduct/{id}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse),400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ApproveProduct(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.ApproveProduct(id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Change product's cover:
        /// 
        ///     PUT
        ///     {
        ///         "id": 0, (id of an image you want it to be the cover)
        ///         "url": "string", (does not matter in this function)
        ///         "isCover": true, (must be TRUE)
        ///         "isVideo" : false, (whether or not this object is a video)
        ///         "isDeleted": false, (must be FALSE)
        ///         "isNewlyAdded": false (must be FALSE)
        ///     }
        ///     
        /// Add new image to product:
        /// 
        ///     PUT
        ///     {
        ///         "id": 0, (not required)
        ///         "url": "string", (required)
        ///         "isCover": true, (could be true, if true the newly added image will be product's new cover)
        ///         "isVideo" : false, (does not matter in this function)
        ///         "isDeleted": false, (must be FALSE)
        ///         "isNewlyAdded": true (must be TRUE)   
        ///     }
        ///     
        /// Delete image:
        /// 
        ///     PUT
        ///     {
        ///         "id": 0, (required)
        ///         "url": "string", ( not required)
        ///         "isCover": false, (must be FALSE)
        ///         "isVideo" : false (does not matter in this function)
        ///         "isDeleted": true, (must be TRUE)
        ///         "isNewlyAdded": false (must be FALSE)   
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been updated successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Edit(long id,ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProductAsync(id,model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// If product already had an order and transaction
        /// it will be soft deleted by setting it status to 3
        /// </remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been deleted successfully</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("mark/{id}")]
        public async Task<IActionResult> Mark(long id)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (user != null)
            {
                userId = user.Value;

                var result = await _productService.AddMarkedProductAsync(userId,id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Oops! Something went wrong");
        }

        [HttpPost("RemoveMark/{id}")]

        public async Task<IActionResult> RemoveMark(long id)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (user != null)
            {
                userId = user.Value;

                var result = await _productService.RemoveMarkedProductAsync(userId, id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Oops! Something went wrong");
        }

        [HttpGet("GetMarkedProduct")]
        public async Task<IActionResult> GetMarkedProduct()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (user != null)
            {
                userId = user.Value;

                var result = await _productService.ListUserMarkedProduct(userId);
                return Ok(result);
            }
            return BadRequest("Oops! Something went wrong");
        }
    }
}
