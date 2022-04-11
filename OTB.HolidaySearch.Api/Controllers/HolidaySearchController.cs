using Microsoft.AspNetCore.Mvc;
using OTB.HolidaySearch.Domain.Contracts;
using OTB.HolidaySearch.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OTB.HolidaySearch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaySearchController : ControllerBase
    {
        private readonly ILogger<HolidaySearchController> logger;
        private readonly IHolidaySearchService holidaySearchService;

        public HolidaySearchController(ILogger<HolidaySearchController> logger, IHolidaySearchService holidaySearchService)
        {
            this.logger = logger;
            this.holidaySearchService = holidaySearchService;
        }

        [HttpPost]
        [SwaggerResponse(200, Description = "Get holiday search results for searched criteria.", Type = typeof(SearchResponse))]
        [Produces("application/json")]
        public async Task<ActionResult> PostAsync(SearchRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var res = this.holidaySearchService.Search(request);
            logger.LogInformation($"returning {res.Recommendations.Count} recommendations");
            return this.Ok(res);
        }
    }
}