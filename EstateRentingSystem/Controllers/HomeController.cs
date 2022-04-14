namespace EstateRentingSystem.Controllers
{
    using System.Linq;
    using EstateRentingSystem.Models.Home;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    
    public class HomeController : Controller
    {
        private readonly IEstateService estates;
        private readonly IStatisticsService statistics;

        public HomeController(
            IEstateService estates,
            IStatisticsService statistics)
        {
            this.estates = estates;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var latestEstates = this.estates.Latest().ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalEstates = totalStatistics.TotalEstates,
                TotalUsers = totalStatistics.TotalUsers,
                Estates = latestEstates
            });
        }

        public IActionResult Error() => View();
    }
}
