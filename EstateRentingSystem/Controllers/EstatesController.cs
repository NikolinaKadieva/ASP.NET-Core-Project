namespace EstateRentingSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Infrastructure;
    using EstateRentingSystem.Models.Estates;
    using EstateRentingSystem.Services.Estates;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    public class EstatesController : Controller
    {
        private readonly IEstateService estates;
        private readonly EstateRentingDbContext data;

        public EstatesController(IEstateService estates, EstateRentingDbContext data)
        {
            this.estates = estates;
            this.data = data;
        }

        public IActionResult All([FromQuery]AllEstatesQueryModel query)
        {
            var queryResult = this.estates.All(
                query.Type,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllEstatesQueryModel.EstatesPerPage);

            var estateTypes = this.estates.AllEstateTypes();

            query.Types = estateTypes;
            query.TotalEstates = queryResult.TotalEstates;
            query.Estates = queryResult.Estates;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new AddEstateFormModel
            {
                Categories = this.GetCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddEstateFormModel estate)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

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
                CategoryId = estate.CategoryId,
                DealerId = dealerId
            };

            this.data.Estates.Add(estateData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsDealer()
            => this.data
                .Dealers
                .Any(d => d.UserId == this.User.GetId());
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
