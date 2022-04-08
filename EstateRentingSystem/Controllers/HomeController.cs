namespace EstateRentingSystem.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Models.Home;
    using EstateRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IConfigurationProvider mapper;
        private readonly EstateRentingDbContext data;

        public HomeController(
            IStatisticsService statistics,
            IMapper mapper,
            EstateRentingDbContext data)
        {
            this.statistics = statistics;
            this.mapper = mapper.ConfigurationProvider;
            this.data = data;            
        }

        public IActionResult Index()
        {
            var estates = this.data
                .Estates
                .OrderByDescending(e => e.Id)
                .ProjectTo<EstateIndexViewModel>(this.mapper)
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
