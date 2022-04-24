namespace EstateRentingSystem.Controllers
{
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Infrastructure.Extensions;
    using EstateRentingSystem.Models.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;
    public class DealersController : Controller
    {
        private readonly EstateRentingDbContext data;

        public DealersController(EstateRentingDbContext data)
           => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]

        public IActionResult Become(BecomeDealerFormModel dealer)
        {
            var userId = this.User.Id();

            var userIsAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId,
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becomming a dealer!";

            return RedirectToAction(nameof(EstatesController.Add), "Estates");
        }

        [Authorize]
        public IActionResult Contacts(int dealerId)
        {
            var dealer = this.data.Dealers.Where(d => d.Id == dealerId).Select(x => new ContactDealerFormModel
            {
                Name = x.Name,
                PhoneNumber= x.PhoneNumber
            }).FirstOrDefault();

            return View(dealer);
        }
    }
}
