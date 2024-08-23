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
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        /// <summary>
        /// Search product
        /// </summary>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Search all:
        /// 
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": 0,
        ///         "brandId": 0
        ///     }
        /// 
        /// Search by brand:
        ///     
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": 0,
        ///         "brandId": {brandId}
        ///     }
        ///     
        /// Search by category:
        /// 
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": {categoryId},
        ///         "brandId": 0
        ///     }
        ///     
        /// 
        /// Search by brand and category:
        /// 
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": {categoryId},
        ///         "brandId": {brandId}
        ///     }
        /// 
        /// </remarks>
        /// <response code ="200">Search result</response>
        /// <response code ="400">Not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(typeof(List<Product>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int page, int size,SearchViewModel model)
        {
            List<Product> result = new List<Product>();
            if (model.CategoryId != 0 && model.BrandId != 0)
            {
                result = await _searchService.SearchProductByBrandAndCategory(model);
                if (result == null)
                    return BadRequest("Not found");
            }
            else if (model.CategoryId == 0 && model.BrandId != 0)
            {
                result = await _searchService.SearchProductByBrand(model);
                if (result == null)
                    return BadRequest("Not found");
            }
            else if (model.CategoryId != 0 && model.BrandId == 0)
            {
                result = await _searchService.SearchProductByCategory(model);
                if (result == null)
                    return BadRequest("Not found");
            }
            else
            {
                result = await _searchService.SearchAllProduct(model);
                if (result == null)
                    return BadRequest("Not found");
            }

            if (page < 1)
            {
                page = 1;
            }

            int recsCount = result.Count();

            var pager = new Pager(recsCount, page, size);

            var recSkip = (page - 1) * size;

            var data = new
            {
                ProductList = result.Skip(recSkip).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            return Ok(data);
        }
    }
}
