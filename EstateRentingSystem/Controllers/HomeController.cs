namespace EstateRentingSystem.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : Controller
    {
        private readonly EstateRentingDbContext data;

        public HomeController(EstateRentingDbContext data)
            => this.data = data;
        public IActionResult Index()
        {
            var totalEstates = this.data.Estates.Count();

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

            return View(new IndexViewModel
            { 
                TotalEstates = totalEstates,
                Estates = estates
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
