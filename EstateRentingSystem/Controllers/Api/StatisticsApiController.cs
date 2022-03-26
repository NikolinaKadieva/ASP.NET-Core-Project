namespace EstateRentingSystem.Controllers.Api
{

    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Models.Api.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]

    public class StatisticsApiController : ControllerBase
    {
        private readonly EstateRentingDbContext data;

        public StatisticsApiController(EstateRentingDbContext data)
            => this.data = data;

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalEstates = this.data.Estates.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalEstates = totalEstates,
                TotalUsers = totalUsers,
                TotalRents = 0
            };
        }
    }
}
