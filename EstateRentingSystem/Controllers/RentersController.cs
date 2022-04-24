namespace EstateRentingSystem.Controllers
{
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Infrastructure.Extensions;
    using EstateRentingSystem.Models.Renters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    using static WebConstants;
    public class RentersController : Controller
    {
        private readonly EstateRentingDbContext data;

        public RentersController(EstateRentingDbContext data)
           => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]

        public IActionResult Become(BecomeRenterFormModel renter)
        {
            var userId = this.User.Id();

            var userIsAlreadyDealer = this.data
                .Renters
                .Any(d => d.UserId == userId);

            if (userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(renter);
            }

            var renterData = new Renter
            {
                Name = renter.Name,
                PhoneNumber = renter.PhoneNumber,
                UserId = userId,
            };

            this.data.Renters.Add(renterData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becomming a renter!";

            return RedirectToAction(nameof(EstatesController.All), "Estates");
        }

        [Authorize]
        public IActionResult Contacts(int renterId)
        {
            var renter = this.data.Renters.Where(d => d.Id == renterId).Select(x => new ContactRenterFormModel
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).FirstOrDefault();

            return View(renter);
        }
    }
}
