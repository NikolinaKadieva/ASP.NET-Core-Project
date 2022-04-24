namespace EstateRentingSystem.Controllers.Api
{
    using EstateRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisctics;

        public StatisticsApiController(IStatisticsService statisctics)
            => this.statisctics = statisctics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics() 
            => this.statisctics.Total();
    }
}