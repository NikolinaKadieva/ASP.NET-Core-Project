namespace EstateRentingSystem.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Estates.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static WebConstants.Cache;
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
            var latestEstates = this.cache.Get<List<LatestEstateServiceModel>>(LatestEstatesCacheKey);

            if (latestEstates == null)
            {
                latestEstates = this.estates
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(LatestEstatesCacheKey, latestEstates, cacheOptions);
            }

            return View(latestEstates);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Error() => View();
    }
}