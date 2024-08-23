using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.ViewModel;
using MobileShopAPI.Services;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService,ApplicationDbContext context)
        {
            _context = context;
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Get all posts by Category in Day
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInDay")]
        public async Task<IActionResult> getPostsByCategoryInDay()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInDay());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by Category in Week
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInWeek")]
        public async Task<IActionResult> getPostsByCategoryInWeek()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInWeek());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by Category in Month
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInMonth")]
        public async Task<IActionResult> getPostsByCategoryInMonth()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInMonth());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get all posts by Category in stages
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("getPostsByCategoryInStages")]
        public async Task<IActionResult> getPostsByCategoryInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by User in Day
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInDay")]
        public async Task<IActionResult> getPostsByUserInDay()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInDay());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by User in Week
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInWeek")]
        public async Task<IActionResult> getPostsByUserInWeek()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInWeek());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by User in Month
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInMonth")]
        public async Task<IActionResult> getPostsByUserInMonth()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInMonth());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get all posts by User in stages
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("getPostsByUserInStages")]
        public async Task<IActionResult> getPostsByUserInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all Users by Post amount in stages
        /// </summary>
        /// <response code ="200">Get all user</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("getUserByPostInStages")]
        public async Task<IActionResult> getUserByPostInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetUserByPostInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all Users by Package Purchases amount in stages
        /// </summary>
        /// <response code ="200">Get all user</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("getUserByPackagePurchasesInStages")]
        public async Task<IActionResult> getUserByPackagePurchasesInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetUserByPackagePurchasesInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// The number of new users by month of the year
        /// </summary>
        /// <response code ="200">New users by month of the year</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("monthly-registers")]
        public async Task<IActionResult> GetMonthlyNewRegisters(int year)
        {
            var data = await _context.Users.Where(x => x.CreatedDate.Value.Date.Year == year)
               .GroupBy(x => x.CreatedDate.Value.Date.Month)
               .Select(g => new MonthlyNewUsersViewModel()
               {
                   Month = g.Key,
                   NumberOfNewUser = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// The number of new users during the period
        /// </summary>
        /// <response code ="200">The number of new users during the period</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("FromTo-registers")]
        public async Task<IActionResult> GetNewRegisters(string fromDate, string toDate)
        {
           
            var query = from u in _context.Users
                        select new
                        {
                            CreateDate = u.CreatedDate,
                            UserId = u.Id,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = await query.GroupBy(x => x.CreateDate)
                                .Select(g => new WeeklyNewUserViewModel()
                                {
                                    NumberOfNewUser = g.Count()
                                })
                                .ToListAsync();
            return Ok(result);
        }

        // product



        /// <summary>
        /// The number of new products by month of the year
        /// </summary>
        /// <response code ="200">New products by month of the year</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("monthly-product")]
        public async Task<IActionResult> GetMonthlyNewProducts(int year)
        {
            var data = await _context.Products.Where(x => x.CreatedDate.Value.Date.Year == year)
               .GroupBy(x => x.CreatedDate.Value.Date.Month)
               .Select(g => new MonthlyNewProductViewModel() 
               {
                   Month = g.Key,
                   NumberOfNewProduct = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// The number of new products during the period
        /// </summary>
        /// <response code ="200">The number of new products during the period</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("FromTo-Products")]
        public async Task<IActionResult> GetNewProducts(string fromDate, string toDate)
        {

            var query = from u in _context.Products
                        select new
                        {
                            CreateDate = u.CreatedDate,
                            ProductId = u.Id,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = await query.GroupBy(x => x.CreateDate)
                                .Select(g => new FromToNewProductViewModel() 
                                {
                                    NumberOfNewProduct = g.Count()
                                })
                                .ToListAsync();
            return Ok(result);
        }


        //report



        /// <summary>
        /// The number of new reports by month of the year
        /// </summary>
        /// <response code ="200">New reports by month of the year</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("monthly-reports")]
        public async Task<IActionResult> GetMonthlyNewReports(int year)
        {
            var data = await _context.Reports.Where(x => x.CreatedDate.Value.Date.Year == year)
               .GroupBy(x => x.CreatedDate.Value.Date.Month)
               .Select(g => new MonthlyNewReportViewModel() 
               {
                   Month = g.Key,
                   NumberOfNewReport = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }


        /// <summary>
        /// The number of new reports during the period
        /// </summary>
        /// <response code ="200">The number of new reports during the period</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("FromTo-reports")]
        public async Task<IActionResult> GetNewReports(string fromDate, string toDate)
        {

            var query = from u in _context.Reports
                        select new
                        {
                            CreateDate = u.CreatedDate,
                            ReportId = u.Id,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = await query.GroupBy(x => x.CreateDate)
                                .Select(g => new FromToNewReportViewModel() 
                                {
                                    NumberOfNewReport = g.Count()
                                })
                                .ToListAsync();
            return Ok(result);
        }
    }
}
