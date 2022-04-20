namespace EstateRentingSystem.Areas.Admin.Controllers
{
    using EstateRentingSystem.Services.Estates;
    using Microsoft.AspNetCore.Mvc;

    public class EstatesController : AdminController
    {
        private readonly IEstateService estates;

        public EstatesController(IEstateService estates) => this.estates = estates;

        public IActionResult All()
        {
            var estates = this.estates.All(publicOnly: false).Estates;
            return View(estates);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.estates.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}