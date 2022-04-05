namespace EstateRentingSystem.Controllers
{
    using EstateRentingSystem.Infrastructure;
    using EstateRentingSystem.Models.Estates;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class EstatesController : Controller
    {
        private readonly IEstateService estates;
        private readonly IDealerService dealers;

        public EstatesController(
            IEstateService estates,
            IDealerService dealers)
        {
            this.estates = estates;
            this.dealers = dealers;
        }

        public IActionResult All([FromQuery]AllEstatesQueryModel query)
        {
            var queryResult = this.estates.All(
                query.Type,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllEstatesQueryModel.EstatesPerPage);

            var estateTypes = this.estates.AllTypes();

            query.Types = estateTypes;
            query.TotalEstates = queryResult.TotalEstates;
            query.Estates = queryResult.Estates;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myEstates = this.estates.ByUser(this.User.Id());

            return View(myEstates);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.Id()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new EstateFormModel
            {
                Categories = this.estates.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(EstateFormModel estate)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.estates.CategoryExists(estate.CategoryId))
            {
                this.ModelState.AddModelError(nameof(estate.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                estate.Categories = this.estates.AllCategories();

                return View(estate);
            }

            this.estates.Create(
                estate.Type,
                estate.TypeOfConstruction,
                estate.Description,
                estate.YearOfConstruction,
                estate.Squaring,
                estate.ImageUrl,
                estate.CategoryId,
                dealerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]

        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var estate = this.estates.Details(id);

            if (estate.UserId != userId) 
            {
                return Unauthorized();
            }

            return View(new EstateFormModel
            {
                Type = estate.Type,
                TypeOfConstruction = estate.TypeOfConstruction,
                Description = estate.Description,
                YearOfConstruction = estate.YearOfConstruction,
                Squaring = estate.Squaring,
                ImageUrl = estate.ImageUrl,
                CategoryId = estate.CategoryId,
                Categories = this.estates.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EstateFormModel estate)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.estates.CategoryExists(estate.CategoryId))
            {
                this.ModelState.AddModelError(nameof(estate.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                estate.Categories = this.estates.AllCategories();

                return View(estate);
            }

            if (!this.estates.IsByDealer(id, dealerId))
            {
                return BadRequest();
            }

            this.estates.Edit(
                id,
                estate.Type,
                estate.TypeOfConstruction,
                estate.Description,
                estate.YearOfConstruction,
                estate.Squaring,
                estate.ImageUrl,
                estate.CategoryId);

            return RedirectToAction(nameof(All));
        }
    }
}
