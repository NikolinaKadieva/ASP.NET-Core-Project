namespace EstateRentingSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class EstatesController : AdminController
    {
        public IActionResult Index() => View();
    }
}