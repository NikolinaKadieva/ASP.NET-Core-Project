namespace EstateRentingSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Estates.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : Controller
    {
        private readonly IEstateService estates;
        private readonly IMemoryCache cache;

        public HomeController(
            IEstateService estates,
            IMemoryCache cache)
        {
            this.estates = estates;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string LatestEstatesCacheKey = "LatesteEstatesCacheKey";

            var latestEstates = this.cache.Get<List<LatestEstateServiceModel>>(LatestEstatesCacheKey);

            if (latestEstates == null)
            {
                latestEstates = this.estates
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(System.TimeSpan.FromMinutes(15));

                this.cache.Set(LatestEstatesCacheKey, latestEstates, cacheOptions);
            }

            return View(latestEstates);
        }

        public IActionResult Error() => View();
    }
}
