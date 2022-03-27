namespace EstateRentingSystem.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Models.Home;
    using EstateRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly EstateRentingDbContext data;

        public HomeController(
            IStatisticsService statistics,
            EstateRentingDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var estates = this.data
                .Estates
                .OrderByDescending(e => e.Id)
                .Select(e => new EstateIndexViewModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    TypeOfConstruction = e.TypeOfConstruction,
                    YearOfConstruction = e.YearOfConstruction,
                    Squaring = e.Squaring,
                    ImageUrl = e.ImageUrl
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            { 
                TotalEstates = totalStatistics.TotalEstates,
                TotalUsers = totalStatistics.TotalUsers,
                Estates = estates
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
