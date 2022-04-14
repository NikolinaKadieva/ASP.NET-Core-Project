namespace EstateRentingSystem.Controllers
{
    using EstateRentingSystem.Infrastructure;
    using EstateRentingSystem.Models.Estates;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;

    public class EstatesController : Controller
    {
        private readonly IEstateService estates;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public EstatesController(
            IEstateService estates,
            IDealerService dealers,
            IMapper mapper)
        {
            this.estates = estates;
            this.dealers = dealers;
            this.mapper = mapper;
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
                Categories = this.estates.AllCategories(),
                Furnitures = this.estates.AllFurnitures(),
                Animals = this.estates.AllAnimals()
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

            if (!this.estates.FurnitureExists(estate.FurnitureId))
            {
                this.ModelState.AddModelError(nameof(estate.FurnitureId), "Furniture does not exist.");
            }
            if (!ModelState.IsValid)
            {
                estate.Furnitures = this.estates.AllFurnitures();

                return View(estate);
            }

            if (!this.estates.AnimalExists(estate.AnimalId))
            {
                this.ModelState.AddModelError(nameof(estate.AnimalId), "Animal does not exist.");
            }
            if (!ModelState.IsValid)
            {
                estate.Animals = this.estates.AllAnimals();

                return View(estate);
            }

            this.estates.Create(
                estate.Type,
                estate.TypeOfConstruction,
                estate.Description,
                estate.YearOfConstruction,
                estate.Squaring,
                estate.ImageUrl,
                estate.FurnitureId,
                estate.AnimalId,
                estate.CategoryId,
                dealerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]

        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var estate = this.estates.Details(id);

            if (estate.UserId != userId && !User.IsAdmin()) 
            {
                return Unauthorized();
            }

            var estateForm = this.mapper.Map<EstateFormModel>(estate);

            estateForm.Furnitures = this.estates.AllFurnitures();

            estateForm.Animals = this.estates.AllAnimals();

            estateForm.Categories = this.estates.AllCategories();          

            return View(estateForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EstateFormModel estate)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0 && !User.IsAdmin())
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

                estate.Furnitures = this.estates.AllFurnitures();

                estate.Animals = this.estates.AllAnimals();

                return View(estate);
            }

            if (!this.estates.IsByDealer(id, dealerId) && !User.IsAdmin())
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
                estate.FurnitureId,
                estate.AnimalId,
                estate.CategoryId);

            return RedirectToAction(nameof(All));
        }
    }
}
