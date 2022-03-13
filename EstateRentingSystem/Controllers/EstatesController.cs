namespace EstateRentingSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models.Estates;
    using Microsoft.AspNetCore.Mvc;
    public class EstatesController : Controller
    {
        private readonly EstateRentingDbContext data;

        public EstatesController(EstateRentingDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddEstateFormModel
        {
            Categories = this.GetCategories()
        });

        public IActionResult All()
        {
            var estates = this.data
                .Estates
                .OrderByDescending(e => e.Id)
                .Select(e => new EstateListingViewModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    TypeOfConstruction = e.TypeOfConstruction,
                    YearOfConstruction = e.YearOfConstruction,
                    Squaring = e.Squaring,
                    ImageUrl = e.ImageUrl,
                    Category = e.Category.Name
                })
                .ToList();

            return View(estates);
        }

        [HttpPost]
        public IActionResult Add(AddEstateFormModel estate)
        {
            if (!this.data.Categories.Any(e => e.Id == estate.CategoryId))
            {
                this.ModelState.AddModelError(nameof(estate.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                estate.Categories = this.GetCategories();

                return View(estate);
            }

            var estateData = new Estate
            {
                Type = estate.Type,
                TypeOfConstruction = estate.TypeOfConstruction,
                Description = estate.Description,
                YearOfConstruction = estate.YearOfConstruction,
                Squaring = estate.Squaring,
                ImageUrl = estate.ImageUrl,
                CategoryId = estate.CategoryId
            };

            this.data.Estates.Add(estateData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<EstateCategoryViewModel> GetCategories()
            => this.data
                .Categories
                .Select(e => new EstateCategoryViewModel
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToList();
    }
}
